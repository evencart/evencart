using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RoastedMarketplace.Services.VerboseReporter
{
    /// <summary>
    /// Interface for sending verbose error or success messages with response
    /// </summary>
    public interface IVerboseReporterService
    {
        /// <summary>
        /// Sends the error string with http response
        /// </summary>
        /// <param name="error"></param>
        /// <param name="errorContextName"></param>
        /// <param name="callerName"></param>
        void ReportError(string error, string errorContextName, [CallerMemberName] string callerName = null);

        /// <summary>
        /// Sends the success string with http response
        /// </summary>
        /// <param name="success"></param>
        /// <param name="successContextName"></param>
        void ReportSuccess(string success, string successContextName);

        /// <summary>
        /// Gets the list of errors added so far
        /// </summary>
        /// <returns></returns>
        Dictionary<string, List<string>> GetErrorsList();

        /// <summary>
        /// Gets the list of success messages added so far
        /// </summary>
        /// <returns></returns>
        Dictionary<string, List<string>> GetSuccessList();

        /// <summary>
        /// Returns true if verbose reporter has any errors stored
        /// </summary>
        bool HasErrors();

        /// <summary>
        /// Returns true if verbose reporter has any error with named context
        /// </summary>
        /// <param name="errorContextName"></param>
        /// <returns></returns>
        bool HasErrors(string errorContextName);

    }
}