namespace Dorsavi.Xamarin.Forms.Services
{
    using Dorsavi.Xamarin.Forms.Models;
    using global::SQLite;
    using global::SQLiteNetExtensions.Extensions;
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;

    public class Database
    {
        public SQLiteConnection _connection { get; set; }
        public Database(SQLiteConnection connection)
        {
            if (_connection == null)
                _connection = connection;

            ConfigureSQLiteConnection();

            //Create the Tables based on the Schema on Each Entity
            GenerateTablesOnInitialMigration();
        }

        private void ConfigureSQLiteConnection()
        {
            _connection.BusyTimeout = new TimeSpan(1, 0, 0); //Enforce Thread Safety by increasing the timeout
        }

        private void GenerateTablesOnInitialMigration()
        {
            _connection.CreateTable<DorsaviItems>();
            _connection.CreateTable<DorsaviPetItems>();
            _connection.CreateTable<LogItems>();
        }

        #region Api (Business Logic)
        //GETS
        public IEnumerable<PersistentType> GetAll<PersistentType>(string query) where PersistentType : class, new() => _connection.CreateCommand(query).ExecuteQuery<PersistentType>().AsEnumerable();
        public List<KeyValuePair<DorsaviItems, DorsaviPetItems>> GetEntireCollectionOfData(IEnumerable<int> petIds)
        {
            List<KeyValuePair<DorsaviItems, DorsaviPetItems>> itemCollection = new List<KeyValuePair<DorsaviItems, DorsaviPetItems>>();
            var queriedPets = this.GetAll<DorsaviPetItems>($"SELECT * FROM {nameof(DorsaviPetItems)} WHERE Id IN ({string.Join<int>(",", petIds)})");
            if (queriedPets != null && queriedPets.Count() != 0)
            {
                var fetchedPetsViaOneToMany = this._connection.GetAllWithChildren<DorsaviPetItems>((e) => petIds.Contains(e.Id));
                foreach (var petItem in fetchedPetsViaOneToMany)
                    itemCollection.Add(new KeyValuePair<DorsaviItems, DorsaviPetItems>(petItem.ParentItem, petItem));
            }

            return itemCollection;
        }

        //INSERTS
        public void InsertItemsWithChildren<T>(IEnumerable<T> items) => _connection.InsertAllWithChildren(items);
        public void Insert<T>(T objectToInsert) => _connection.Insert(objectToInsert);
        public int InsertItems<T>(IEnumerable<T> items) => _connection.InsertAll(items);
        public void InsertOrUpdate<T>(T obj) => _connection.InsertOrReplace(obj);

        //TRANSACTION MANAGEMENT
        public void Rollback() => _connection.Rollback();
        public void RunInTransaction(Action action) => _connection.RunInTransaction(() => { action.Invoke(); });
        #endregion
    }
}
