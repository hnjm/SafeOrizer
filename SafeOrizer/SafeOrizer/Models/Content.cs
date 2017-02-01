// <copyright file="Content.cs" company="Christoph Nienaber, https://github.com/zuckerthoben">
// Copyright (c) Christoph Nienaber, https://github.com/zuckerthoben. All rights reserved.
// </copyright>

namespace SafeOrizer.Models
{
    using System;
    using SafeOrizer.Interfaces;
    using SQLite;

    /// <summary>
    /// The model for the data that is saved in the SQLite database
    /// </summary>
    public class Content : IEncryptable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Content"/> class.
        /// </summary>
        public Content()
        {
            this.DateAdded = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the filename
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the binary data
        /// </summary>
        public byte[] EncryptableData { get; set; }

        /// <summary>
        /// Gets or sets the IV for AES encryption/decryption
        /// </summary>
        public byte[] IV { get; set; }

        /// <summary>
        /// Gets or sets the Hash of the data to verify successful encryption/decryption
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets the filetype of the data added
        /// </summary>
        public FileType FileType { get; set; }

        /// <summary>
        /// Gets or sets the date the data was added
        /// </summary>
        public DateTime DateAdded { get; set; }

        public byte[] GetIV() => this.IV;
        public void SetIV(byte[] iv) => this.IV = iv;
        public byte[] GetData() => this.EncryptableData;
        public void SetData(byte[] data) => this.EncryptableData = data;
    }

    public enum FileType
    {
        Image,
        Video,
        File
    }

}
