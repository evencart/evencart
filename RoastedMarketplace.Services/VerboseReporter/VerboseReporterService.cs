using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RoastedMarketplace.Services.VerboseReporter
{
    public class VerboseReporterService : IVerboseReporterService
    {
        private readonly Dictionary<string, List<string>> _verboseErrorMessages;
        private readonly Dictionary<string, List<string>> _verboseSuccessMessages;

        public VerboseReporterService()
        {
            _verboseErrorMessages = new Dictionary<string, List<string>>();
            _verboseSuccessMessages = new Dictionary<string, List<string>>(); 
        }

        public void ReportError(string error, string errorContextName, [CallerMemberName] string callerName = null)
        {
            //todo: use callername to keep track and log the error sources
            if(!_verboseErrorMessages.ContainsKey(errorContextName))
                _verboseErrorMessages.Add(errorContextName, new List<string>());

            _verboseErrorMessages[errorContextName].Add(error);
        }

        public void ReportSuccess(string success, string successContextName)
        {
            if (!_verboseSuccessMessages.ContainsKey(successContextName))
                _verboseSuccessMessages.Add(successContextName, new List<string>());

            _verboseSuccessMessages[successContextName].Add(success);
        }

        public Dictionary<string, List<string>> GetErrorsList()
        {
            return _verboseErrorMessages;
        }

        public Dictionary<string, List<string>> GetSuccessList()
        {
            return _verboseSuccessMessages;
        }

        public bool HasErrors()
        {
            return _verboseErrorMessages.Any();
        }

        public bool HasErrors(string errorContextName)
        {
            return _verboseErrorMessages.Any(x => x.Key == errorContextName && x.Value.Any());
        }

    }
}