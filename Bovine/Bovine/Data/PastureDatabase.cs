using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bovine.Models;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensionsAsync.Extensions;

namespace Bovine.Data
{
    public class PastureDatabase
    {
        readonly SQLiteAsyncConnection _connection;

        public PastureDatabase(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Pasture>().Wait();
        }

        public Task<List<Pasture>> GetPasturesAsync()
        {
            return _connection.Table<Pasture>().ToListAsync();
        }

        public Task<int> SavePastureAsync(Pasture pasture)
        {
            if (pasture.ID != 0)
            {
                return _connection.UpdateAsync(pasture);
            }
            else
            {
                
                return _connection.InsertAsync(pasture);
            }
        }

        public Task<int> DeletePastureAsync(Pasture pasture)
        {
            return _connection.DeleteAsync(pasture);
        }

        public Task<List<Pasture>> GetPasturesAsyncExtensions()
        {
            return _connection.GetAllWithChildrenAsync<Pasture>();
        }

        public Task<List<Pasture>> GetPasturesByFarmAsyncExtensions(Farm farm)
        {
            //return _connection.GetAllWithChildrenAsync<Pasture>(x => farm.ID == x.FarmID);
            var past = _connection.Table<Pasture>().Where(p => p.FarmID == farm.ID).ToListAsync();
            return past;
        }

        public Task<Pasture> GetPastureByIdAsyncExtensions(Pasture pasture)
        {
            return _connection.GetWithChildrenAsync<Pasture>(pasture.ID);
        }

        public Task SavePastureAsyncExtensions(Pasture pasture)
        {
            if (pasture.ID != 0)
            {
                return _connection.UpdateWithChildrenAsync(pasture);
            }
            else
            {

                return _connection.InsertWithChildrenAsync(pasture);
            }
        }

        public Task<int> DeletePastureAsyncExtensions(Pasture pasture)
        {
            return _connection.DeleteAsync(pasture);
        }
    }
}
