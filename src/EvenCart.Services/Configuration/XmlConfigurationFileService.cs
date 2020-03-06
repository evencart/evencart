#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System.Collections.Generic;
using System.IO;
using System.Xml;
using EvenCart.Core.Services.Configuration;

namespace EvenCart.Services.Configuration
{
    public class XmlConfigurationFileService : IConfigurationFileService
    {
        public IDictionary<string, string> ReadFile(string filePath, bool throwExceptionIfFileNotFound = false)
        {
            var fileFound = File.Exists(filePath);

            if (!fileFound)
                if (throwExceptionIfFileNotFound)
                    throw new FileNotFoundException();

            if (!fileFound)
                return null;

            var configurationValues = new Dictionary<string, string>();
            //read the file stream
            var fileStream = new FileStream(filePath, FileMode.Open);
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
                        currentConfigName = xmlReader.Name;
                        break;
                    case XmlNodeType.Text:
                        if (currentConfigName != string.Empty && configurationValues.ContainsKey(currentConfigName))
                            configurationValues[currentConfigName] = xmlReader.Value;
                        else
                            configurationValues.Add(currentConfigName, xmlReader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        currentConfigName = string.Empty;
                        break;
                }
            }
            //close the stream
            fileStream.Close();

            return configurationValues;
            
        }

        public void WriteFile(string savePath, IDictionary<string, string> configurationValues, bool overwriteIfFileExists = true)
        {
            var fileExists = File.Exists(savePath);

            if(fileExists && !overwriteIfFileExists)
                throw new IOException("File already exists");

            var fileStream = new FileStream(savePath, FileMode.Create);
            var xmlWriterSettings = new XmlWriterSettings {Indent = true};

            var xmlWriter = XmlTextWriter.Create(fileStream, xmlWriterSettings);
            
            //<!?xml>
            xmlWriter.WriteStartDocument();

            //<config>
            xmlWriter.WriteStartElement("config");

            foreach (KeyValuePair<string, string> values in configurationValues)
            {
                //<key>
                xmlWriter.WriteStartElement(values.Key);

                //key name
                xmlWriter.WriteString(values.Value);

                //</key>
                xmlWriter.WriteEndElement();
            }

            //</config>
            xmlWriter.WriteEndElement();

            //close the writer
            xmlWriter.Close();

            //close the stream
            fileStream.Close();
        }
    }
}