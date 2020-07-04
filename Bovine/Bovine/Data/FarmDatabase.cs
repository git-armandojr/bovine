using Bovine.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bovine.Data
{
    public class FarmDatabase
    {
        readonly SQLiteAsyncConnection _connection;

        public FarmDatabase(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Farm>().Wait();
        }

        public Task<List<Farm>> GetFarmsAsync()
        {
            return _connection.Table<Farm>().ToListAsync();
        }

        public Task<Farm> GetFarmAsync(int id)
        {
            return _connection.Table<Farm>().FirstOrDefaultAsync(f => f.ID == id);
        }

        public Task<int> SaveFarmAsync(Farm farm)
        {
            if(farm.ID != 0)
            {
                return _connection.UpdateAsync(farm);
            }
            else
            {
                return _connection.InsertAsync(farm);
            }
        }

        public Task<int> DeleteFarmAsync(Farm farm)
        {
            return _connection.DeleteAsync(farm);
        }
    }
}
