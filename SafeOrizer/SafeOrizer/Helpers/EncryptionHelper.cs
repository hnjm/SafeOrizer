// <copyright file="EncryptionHelper.cs" company="Christoph Nienaber, https://github.com/zuckerthoben">
// Copyright (c) Christoph Nienaber, https://github.com/zuckerthoben. All rights reserved.
// </copyright>

namespace SafeOrizer.Helpers
{
    using System;
    using PCLCrypto;
    using SafeOrizer.Interfaces;

    /// <summary>
    /// Implements PCLCrypto AES encryption
    /// </summary>
    public static class EncryptionHelper
    {
        /// <summary>
        /// Creates a random generated salt that contains 16 bytes
        /// </summary>
        /// <returns>the randomly generated salt </returns>
        public static byte[] CreateSalt()
        {
            var salt = new byte[15];
            NetFxCrypto.RandomNumberGenerator.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// Generates a random uint
        /// </summary>
        /// <returns>the randomly generated uint</returns>
        public static uint GetRandomUInt()
        {
            var random = WinRTCrypto.CryptographicBuffer.GenerateRandomNumber();
            var randomFrom0To15 = WinRTCrypto.CryptographicBuffer.GenerateRandomNumber() % 16;

            return randomFrom0To15;
        }

        /// <summary>
        /// Gets the hash for the data
        /// </summary>
        /// <param name="data">The data to hash</param>
        /// <param name="hashAlgorithm">the hash algorithm you want to use</param>
        /// <returns>the generated hash</returns>
        public static string GetDataHash(byte[] data, HashAlgorithm hashAlgorithm = HashAlgorithm.Sha1)
        {
            var hasher = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(hashAlgorithm);
            var hash = hasher.HashData(data);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Encrypts the data of the IEncryptable with the key and sets the IV for future decryption
        /// </summary>
        /// <param name="password">The password the user specified</param>
        /// <param name="salt">The generated salt for the user</param>
        /// <param name="encryptable">The object to be encrypted, containing not encrypted data (plaintext)</param>
        /// <returns>The object with encrypted data (ciphertext)</returns>
        public static IEncryptable EncryptData(string password, byte[] salt, IEncryptable encryptable) =>
            EncryptData(DeriveAESEncryptionKeyFromPassword(password, salt), encryptable);

        /// <summary>
        /// Decrypts data from an object with encrypted data
        /// </summary>
        /// <param name="password">The password the user specified</param>
        /// <param name="salt">The generated salt for the user</param>
        /// <param name="encryptable">The object to be decrypted, containing encrypted data (ciphertext)</param>
        /// <returns>The object with decrypted data (plaintext)</returns>
        public static IEncryptable DecryptData(string password, byte[] salt, IEncryptable encryptable) =>
            DecryptData(DeriveAESEncryptionKeyFromPassword(password, salt), encryptable);

        /// <summary>
        /// Encrypts the data of the IEncryptable with the key and sets the IV for future decryption
        /// </summary>
        /// <param name="keyMaterial">The AES encryption key</param>
        /// <param name="encryptable">The object to be encrypted, containing not encrypted data (plaintext)</param>
        /// <returns>The object with encrypted data (ciphertext)</returns>
        private static IEncryptable EncryptData(byte[] keyMaterial, IEncryptable encryptable)
        {
            var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            var key = provider.CreateSymmetricKey(keyMaterial);
            
            var iv = WinRTCrypto.CryptographicBuffer.GenerateRandom(provider.BlockLength);
            var cipherText = WinRTCrypto.CryptographicEngine.Encrypt(key, encryptable.GetData(), iv);

            encryptable.SetData(cipherText);
            encryptable.SetIV(iv);

            return encryptable;
        }

        /// <summary>
        /// Decrypts the data of the IEncryptable with the key and sets the IV for future decryption
        /// </summary>
        /// <param name="keyMaterial">The AES encryption key</param>
        /// <param name="encryptable">The object to be decrypted, containing encrypted data (ciphertext)</param>
        /// <returns>The object with decrypted data (plaintext)</returns>
        private static IEncryptable DecryptData(byte[] keyMaterial, IEncryptable encryptable)
        {
            var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            var key = provider.CreateSymmetricKey(keyMaterial);

            var plainText = WinRTCrypto.CryptographicEngine.Decrypt(key, encryptable.GetData(), encryptable.GetIV());

            encryptable.SetData(plainText);

            return encryptable;
        }

        /// <summary>
        /// Derives a valid AES encryption key from user input
        /// </summary>
        /// <param name="password">The password entered by the user</param>
        /// <param name="salt">The salt that was generated one time for each user</param>
        /// <param name="iterations">The number of iterations to create the key. Higher = more secure</param>
        /// <returns>the valid AES encryption key</returns>
        private static byte[] DeriveAESEncryptionKeyFromPassword(string password, byte[] salt, int iterations = 5000)
        {
            var keyLengthInBytes = 0;
            return NetFxCrypto.DeriveBytes.GetBytes(password, salt, iterations, keyLengthInBytes);
        }
    }
}
