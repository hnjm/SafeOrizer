// <copyright file="IEncryptable.cs" company="Christoph Nienaber, https://github.com/zuckerthoben">
// Copyright (c) Christoph Nienaber, https://github.com/zuckerthoben. All rights reserved.
// </copyright>

namespace SafeOrizer.Interfaces
{
    /// <summary>
    /// Interface to implement Encryption for Models. Used by the EncryptionHelper
    /// </summary>
    public interface IEncryptable
    {
        /// <summary>
        /// Gets or sets the IV used for AES operations
        /// </summary>
        byte[] IV
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Data that should be decrypted/encrypted
        /// </summary>
        byte[] EncryptableData
        {
            get; set;
        }
    }
}
