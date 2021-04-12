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
            database.CreateTableAsync<Opdracht>().Wait();
            database.CreateTableAsync<Klant>().Wait();
            database.CreateTableAsync<Taak>().Wait();
            database.CreateTableAsync<User>().Wait();
        }

        public Task<List<Opdracht>> GetAllOpdrachten()
        {
            return database.Table<Opdracht>().ToListAsync();
        }

        public Task<List<Opdracht>> GetOpdrachten(string id)
        {
            return database.QueryAsync<Opdracht>("SELECT * FROM [Opdracht] WHERE [user] = " + id);
        }

        public Task<Opdracht> GetOpdracht(string id)
        {
            return database.Table<Opdracht>()
                            .Where(i => i.id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> UpdateOpdracht(Opdracht opdracht)
        {
            return database.UpdateAsync(opdracht);
        }

        public Task<int> SaveOpdracht(Opdracht opdracht)
        {
            return database.InsertAsync(opdracht);
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

        public Task<List<Taak>> GetAllTaken()
        {
            return database.Table<Taak>().ToListAsync();
        }
        
        public Task<List<Taak>> GetTaken(string id)
        {
            return database.QueryAsync<Taak>("SELECT * FROM [Taak] WHERE [opdracht] = " + id);
        }

        public Task<Taak> GetTaak(string id)
        {
            return database.Table<Taak>()
                            .Where(i => i.id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> UpdateTaak(Taak taak)
        {
            return database.UpdateAsync(taak);
        }

        public Task<int> SaveTaak(Taak taak)
        {
            return database.InsertAsync(taak);
        }

        public Task<int> SaveUser(User user)
        {
            try
            {
                return database.InsertAsync(user);
            }
            catch
            {
                return database.UpdateAsync(user);
            }
        }

        public void Logout()
        {
            var user = database.QueryAsync<User>("DELETE FROM [User]");

        }
        public Task<List<User>> GetUsers()
        {
            return database.Table<User>().ToListAsync();
        }
    }
}