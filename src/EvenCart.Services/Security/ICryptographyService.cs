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

using EvenCart.Data.Enum;

namespace EvenCart.Services.Security
{
    public interface ICryptographyService
    {
        /// <summary>
        /// Creates a salt of specified length
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        string CreateSalt(int size);

        /// <summary>
        /// Gets the hashed password with required salt and algorithm
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="salt"></param>
        /// <param name="passwordFormat"></param>
        /// <returns></returns>
        string GetHashedPassword(string plainText, string salt, PasswordFormat passwordFormat);

        /// <summary>
        /// Gets the name of password format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        string GetPasswordFormatName(PasswordFormat format);

        /// <summary>
        /// Gets a random alphanumeric password
        /// </summary>
        /// <returns></returns>
        string GetRandomPassword(int length = 15);

        /// <summary>
        /// Encrypts the plain text using specified key and salt
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string Encrypt(string plainText, string key, string salt);

        /// <summary>
        /// Decrypts the plain text using specified key and salt
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string Decrypt(string cipherText, string key, string salt);

        /// <summary>
        /// Encrypts plain text with default encryption key & salt application configuration
        /// </summary>
        string Encrypt(string plainText);

        /// <summary>
        /// Encrypts plain text with default encryption key & salt from application configuration
        /// </summary>
        string Decrypt(string cipherText);

        /// <summary>
        /// Gets the MD5 hash of the provided text
        /// </summary>
        string GetMd5Hash(string plainText);

        /// <summary>
        /// Gets a random numeric code of specified length
        string GetNumericCode(int length);
    }
}