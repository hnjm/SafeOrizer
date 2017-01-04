using SQLite;
using System;

namespace SafeOrizer.Models
{
    public class EncryptedData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FileName { get; set; }

        public byte[] Data { get; set; }

        public string Hash { get; set; }

        public FileType FileType { get; set; }

        public DateTime DateAdded { get; set; }

        public EncryptedData()
        {
            this.DateAdded = DateTime.Now;
        }
    }

    public enum FileType
    {
        Image,
        Video,
        File
    }

}
