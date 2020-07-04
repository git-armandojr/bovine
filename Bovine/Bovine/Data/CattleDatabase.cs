using Bovine.Models;
using SQLite;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bovine.Data
{
    public class CattleDatabase
    {
        readonly SQLiteAsyncConnection _connection;

        public CattleDatabase(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Cattle>().Wait();
        }

        public Task<List<Cattle>> GetCattlesAsync()
        {
            return _connection.Table<Cattle>().ToListAsync();
        }

        public Task<int> SaveCattleAsync(Cattle cattle)
        {
            if (cattle.ID != 0)
            {
                return _connection.UpdateAsync(cattle);
            }
            else
            {
                return _connection.InsertAsync(cattle);
            }
        }

        public Task<int> DeleteCattleAsync(Cattle cattle)
        {
            return _connection.DeleteAsync(cattle);
        }
    }
}
