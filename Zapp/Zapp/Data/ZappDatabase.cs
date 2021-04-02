using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Zapp.Models;

namespace Zapp.Data
{
    public class ZappDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ZappDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Klant>().Wait();
        }

        public Task<List<Klant>> GetKlanten()
        {
            return database.Table<Klant>().ToListAsync();
        }

        public Task<Klant> GetKlant(string id)
        {
            return database.Table<Klant>()
                            .Where(i => i.id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> UpdateKlant(Klant klant)
        {
            return database.UpdateAsync(klant);
        }

        public Task<int> SaveKlant(Klant klant)
        {
            return database.InsertAsync(klant);
        }

    }
}