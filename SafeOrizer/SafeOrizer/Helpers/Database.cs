using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SafeOrizer.Models;
using System.Diagnostics;

namespace SafeOrizer.Helpers
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        public Database(string path)
        {
            this.database = new SQLiteAsyncConnection(path);
            this.database.CreateTableAsync<EncryptedData>().Wait();
        }

        public Task<List<EncryptedData>> GetAllItemsAsync() => 
            this.database.Table<EncryptedData>().ToListAsync();

        public Task<List<EncryptedData>> GetImageItemsAsync() 
        {
            var query = this.database.Table<EncryptedData>().Where(d => d.FileType == FileType.Image);

            return query.ToListAsync();
        }

        public Task<List<EncryptedData>> GetVideoItemsAsync()
        {
            var query = this.database.Table<EncryptedData>().Where(d => d.FileType == FileType.Video);

            return query.ToListAsync();
        }

        public Task<EncryptedData> GetItemAsync(int id) => 
            this.database.Table<EncryptedData>().Where(d => d.Id == id).FirstOrDefaultAsync();

        public Task<int> SaveItemAsync(EncryptedData item)
        {
            if (item.Id != 0)
            {
                Debug.WriteLine($"Saving item {item.FileName}");
                return this.database.UpdateAsync(item);
            } else
            {
                Debug.WriteLine($"Inserting item {item.FileName}");
                return this.database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(EncryptedData item) => 
            this.database.DeleteAsync(item);

        public Task<int> DeleteAllItems() =>
            this.database.ExecuteAsync("DELETE FROM EncryptedData");
    }


}
