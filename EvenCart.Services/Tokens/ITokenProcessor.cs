using System.Collections.Generic;

namespace EvenCart.Services.Tokens
{
    public interface ITokenProcessor
    {
        /// <summary>
        /// Gets all the available tokens for the entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        IList<Token> GetAvailableTokens<T>(T entity);




    }
}