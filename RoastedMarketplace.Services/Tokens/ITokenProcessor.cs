using System.Collections.Generic;

namespace RoastedMarketplace.Services.Tokens
{
    public interface ITokenProcessor
    {
        /// <summary>
        /// Replaces all tokens names within content with their respective token values and returns the updated string
        /// </summary>
        /// <param name="content"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        string ProcessTokens(string content, IList<Token> tokens);

        /// <summary>
        /// Gets all the available tokens for the entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        IList<Token> GetAvailableTokens<T>(T entity);

        /// <summary>
        /// Replaces all token names within content for the available entity tokens
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        string ProcessTokens<T>(string content, T entity);

        /// <summary>
        /// Replaces all token names within content for all the passed entities
        /// </summary>
        /// <param name="content"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        string ProcessAllTokens(string content, params object[] entities);
    }
}