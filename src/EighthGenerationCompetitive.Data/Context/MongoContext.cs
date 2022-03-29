using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EighthGenerationCompetitive.Data.Context
{
    public class MongoContext : IMongoContext
    {
        private readonly List<Func<Task>> _commands;

        private bool _disposed = false;

        private IMongoDatabase Database { get; set; }
        public MongoClient MongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }

        public MongoContext(IConfiguration configuration)
        {
            _commands = new List<Func<Task>>();

            string connectionString = configuration.GetSection("MongoSettings").GetSection("Connection").Value;

            string databaseName = configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value;

            MongoClient = new MongoClient(connectionString);

            Database = MongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name) => Database.GetCollection<T>(name);

        public void AddCommand(Func<Task> command) => _commands.Add(command);

        public async Task<int> SaveChangesAsync()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                while (Session != null && Session.IsInTransaction)
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));

                _disposed = true;
            }
        }
    }
}