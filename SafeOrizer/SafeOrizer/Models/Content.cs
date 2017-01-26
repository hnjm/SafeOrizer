using SafeOrizer.Interfaces;
using SQLite;
using System;

namespace SafeOrizer.Models
{
    public class Content : IEncryptable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FileName { get; set; }

        public byte[] Data { get; set; }

        public byte[] IV { get; set; }

        public string Hash { get; set; }

        public FileType FileType { get; set; }

        public DateTime DateAdded { get; set; }

        public Content()
        {
            this.DateAdded = DateTime.Now;
        }

        public byte[] GetIV() => this.IV;
        public void SetIV(byte[] iv) => this.IV = iv;
        public byte[] GetData() => this.Data;
        public void SetData(byte[] data) => this.Data = data;
    }

    public enum FileType
    {
        Image,
        Video,
        File
    }

}
