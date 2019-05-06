using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace EvenCart.Core.Plugins
{
    public class PluginConfigurator
    {
        public class ReadModuleInfo
        {
            public string SystemName { get; set; }

            public bool Installed { get; set; }

            public bool Active { get; set; }
        }

        public static void WriteConfigFile(IList<PluginInfo> modules, string configFilePath)
        {
            //in the config files, only installed modules are written
            var installedModules = modules.Where(x => x.Installed);

            var fileStream = new FileStream(configFilePath, FileMode.Create);

            var xmlWriterSettings = new XmlWriterSettings { Indent = true };

            var xmlWriter = XmlTextWriter.Create(fileStream, xmlWriterSettings);

            //<!?xml>
            xmlWriter.WriteStartDocument();

            //<modules>
            xmlWriter.WriteStartElement("modules");

            foreach (var im in installedModules)
            {
                //<module>
                xmlWriter.WriteStartElement("module");

                //attribute active
                xmlWriter.WriteAttributeString("active", im.Active.ToString());

                //attribute installed
                xmlWriter.WriteAttributeString("installed", im.Installed.ToString());

                //module name
                xmlWriter.WriteString(im.SystemName);

                //</module>
                xmlWriter.WriteEndElement();
            }

            //</modules>
            xmlWriter.WriteEndElement();

            //close the writer
            xmlWriter.Close();

            //close the stream
            fileStream.Close();

        }

        public static IEnumerable<ReadModuleInfo> ReadConfigFile(string configFilePath)
        {
            var fileFound = File.Exists(configFilePath);
            if (!fileFound)
                return null;

            var readModules = new List<ReadModuleInfo>();


            //read the file stream
            var fileStream = new FileStream(configFilePath, FileMode.Open);
            //create the reader
            var xmlReader = XmlReader.Create(fileStream);

            ReadModuleInfo currentInfo = null;

            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xmlReader.Name == "modules")
                            continue;
                        currentInfo = new ReadModuleInfo()
                        {
                            Active = Convert.ToBoolean(xmlReader.GetAttribute("active")),
                            Installed = Convert.ToBoolean(xmlReader.GetAttribute("installed"))
                        };
                        break;
                    case XmlNodeType.Text:
                        if (currentInfo != null)
                            currentInfo.SystemName = xmlReader.Value;
                        break;
                    case XmlNodeType.EndElement:
                        if (currentInfo != null)
                            readModules.Add(currentInfo);
                        currentInfo = null;
                        break;
                }
            }
            //close the stream
            fileStream.Close();

            return readModules;
        }

        public static PluginInfo LoadModuleInfo(string fileName)
        {
            //because this code fires before app startup, we won't be able to use any resolver to read the files
            //TODO: find a better way to do this
            var fileFound = File.Exists(fileName);

            if (!fileFound)
                throw new FileNotFoundException();


            var moduleInfo = new PluginInfo();
            //read the file stream
            var fileStream = new FileStream(fileName, FileMode.Open);
            //create the reader
            var xmlReader = XmlReader.Create(fileStream);
            var currentConfigName = string.Empty;//this stores the element name at any point of time
            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xmlReader.Name.ToLower() == "config")
                            continue;
                        currentConfigName = xmlReader.Name.ToLower();
                        break;
                    case XmlNodeType.Text:
                        if (currentConfigName != string.Empty)
                        {
                            var value = xmlReader.Value;
                            switch (currentConfigName)
                            {
                                case "name":
                                    moduleInfo.Name = value;
                                    break;
                                case "systemname":
                                    moduleInfo.SystemName = value;
                                    break;
                                case "supportedversions":
                                    moduleInfo.SupportedVersions = value.Split(',').ToList();
                                    break;
                                case "author":
                                    moduleInfo.Author = value;
                                    break;
                                case "authoruri":
                                    moduleInfo.AuthorUri = value;
                                    break;
                                case "moduleuri":
                                    moduleInfo.PluginUri = value;
                                    break;
                                case "assemblydllname":
                                    moduleInfo.AssemblyName = value;
                                    break;
                            }
                        }
                        break;
                    case XmlNodeType.EndElement:
                        currentConfigName = string.Empty;
                        break;
                }
            }
            //close the stream
            fileStream.Close();

            //file info
            var fileInfo = new FileInfo(fileName);
            if (fileInfo.DirectoryName != null)
            {
                var assemblyFilePath = Path.Combine(fileInfo.DirectoryName, moduleInfo.AssemblyName);
                moduleInfo.OriginalAssemblyFileInfo = new FileInfo(assemblyFilePath);
            }

            return moduleInfo;
        }

        public static PluginInfo LoadModuleInfo(IPlugin module)
        {
            var moduleInfo = new PluginInfo()
            {
                PluginType = module.GetType(),
                Active = true
            };
            return moduleInfo;
        }
        //loads all the module info files
        public static IEnumerable<PluginInfo> GetAllModulesInfos(DirectoryInfo probeDirectory, IList<ReadModuleInfo> readModules)
        {
            var moduleFiles = probeDirectory.GetFiles("module.config", SearchOption.AllDirectories);
            var modules = moduleFiles.Select(file => LoadModuleInfo(file.FullName)).ToList();

            foreach (var module in modules)
            {
                var readModule = readModules.FirstOrDefault(x => x.SystemName == module.SystemName);
                if (readModule == null) continue;
                //so this is not a new module
                module.Installed = readModule.Installed;
                module.Active = readModule.Active;
            }

            return modules;
        }


    }
}