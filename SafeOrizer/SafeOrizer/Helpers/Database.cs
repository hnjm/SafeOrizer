using System.Collections.Generic;
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
            this.database.CreateTableAsync<Content>().Wait();
        }

        public Task<int> GetItemCount() => this.database.Table<Content>().CountAsync();

        public Task<List<Content>> GetAllItemsAsync() => 
            this.database.Table<Content>().ToListAsync();

        public Task<List<Content>> GetImageItemsAsync() 
        {
            var query = this.database.Table<Content>().Where(d => d.FileType == FileType.Image);

            return query.ToListAsync();
        }

        public Task<List<Content>> GetVideoItemsAsync()
        {
            var query = this.database.Table<Content>().Where(d => d.FileType == FileType.Video);

            return query.ToListAsync();
        }

        public Task<Content> GetItemAsync(int id) => 
            this.database.Table<Content>().Where(d => d.Id == id).FirstOrDefaultAsync();

        public Task<int> SaveItemAsync(Content item)
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

        public Task<int> DeleteItemAsync(Content item) => 
            this.database.DeleteAsync(item);

        public Task<int> DeleteAllItems() =>
            this.database.ExecuteAsync("DELETE FROM EncryptedData");
    }


}
