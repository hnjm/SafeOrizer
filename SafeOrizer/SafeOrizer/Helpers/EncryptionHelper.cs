using PCLCrypto;
using SafeOrizer.Interfaces;
using System;

namespace SafeOrizer.Helpers
{
    public static class EncryptionHelper
    {
        public static byte[] CreateSalt()
        {
            var salt = new byte[15];
            NetFxCrypto.RandomNumberGenerator.GetBytes(salt);
            return salt;
        }

        public static uint GetRandomUInt()
        {
            var random = WinRTCrypto.CryptographicBuffer.GenerateRandomNumber();
            var randomFrom0To15 = WinRTCrypto.CryptographicBuffer.GenerateRandomNumber() % 16;

            return randomFrom0To15;
        }

        private static byte[] DeriveEncryptionKeyFromPassword(string password, byte[] salt, int iterations = 5000)
        {
            var keyLengthInBytes = 0;
            return NetFxCrypto.DeriveBytes.GetBytes(password, salt, iterations, keyLengthInBytes);
        }

        public static string GetDataHash(byte[] data)
        {
            var hasher = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha1);
            var hash = hasher.HashData(data);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Encrypts the data of the IEncryptable with the key and sets the IV for future decryption
        /// </summary>
        /// <param name="keyMaterial"></param>
        /// <param name="encryptable"></param>
        /// <returns></returns>
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

        public static IEncryptable EncryptData(string password, byte[] salt, IEncryptable encryptable) => 
            EncryptData(DeriveEncryptionKeyFromPassword(password, salt), encryptable);

        private static IEncryptable DecryptData(byte[] keyMaterial, IEncryptable encryptable)
        {
            var provider = WinRTCrypto.SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithm.AesCbcPkcs7);
            var key = provider.CreateSymmetricKey(keyMaterial);

            var plainText = WinRTCrypto.CryptographicEngine.Decrypt(key, encryptable.GetData(), encryptable.GetIV());

            encryptable.SetData(plainText);

            return encryptable;
        }

        public static IEncryptable DecryptData(string password, byte[] salt, IEncryptable encryptable) =>
            DecryptData(DeriveEncryptionKeyFromPassword(password, salt), encryptable);

        //// Create a buffer with strong random data
        //byte[] cryptoRandomBuffer = new byte[15];
        //NetFxCrypto.RandomNumberGenerator.GetBytes(cryptoRandomBuffer);

        //// Generate a random number
        //uint randomFrom0To15 = WinRTCrypto.CryptographicBuffer.GenerateRandomNumber() % 16;

        //// Generate a HMAC for the data
        //        byte[] keyMaterial;
        //        byte[] data;
        //        var algorithm = WinRTCrypto.MacAlgorithmProvider.OpenAlgorithm(MacAlgorithm.HmacSha1);
        //        CryptographicHash hasher = algorithm.CreateHash(keyMaterial);
        //        hasher.Append(data);
        //        byte[] mac = hasher.GetValueAndReset();
        //        string macBase64 = Convert.ToBase64String(mac);
    }
}
