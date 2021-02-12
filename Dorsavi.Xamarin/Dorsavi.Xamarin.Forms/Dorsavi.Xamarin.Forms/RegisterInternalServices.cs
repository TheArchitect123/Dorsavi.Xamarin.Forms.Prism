using AutoMapper;
using Prism.Ioc;
using SQLite;
using System.IO;

namespace Dorsavi.Xamarin.Forms
{
    using Dorsavi.Xamarin.Forms.Constants;
    using Dorsavi.Xamarin.Forms.Mappers;
    using Dorsavi.Xamarin.Forms.Services;
    using Dorsavi.Xamarin.Forms.Services.HttpClients;
    using Dorsavi.Xamarin.Forms.Shared.Extensions;

    /// <summary>
    /// Register all services other than view models into the iOC Container (AutoMapper, HttpConsumers & Client Factory, etc)
    /// </summary>
    internal static class RegisterInternalServices
    {
        public static void RegisterHttpClientServices()
        {
            RegisterWebServices.InitializeClientFactory(); //Register the HttpClientFactory 
        }

        public static void RegisterProfileMappingServices(ref IContainerRegistry container)
        {
            MapperConfiguration appConfig = new MapperConfiguration(c => c.AddProfile<DorsaviProfile>());
            container.Register<IMapper>(c => appConfig.CreateMapper());
        }

        /// <summary>
        /// Used for encrypting the data on a local sqlite file after it has been fetched from the remote server
        /// </summary>
        /// <param name="container"></param>
        public static void RegisterSqliteEncryption(ref IContainerRegistry container)
        {
            //Initialize the Db directory, including access  
            string directoryPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), ApplicationData.DirectoryName);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string dbPath = Path.Combine(directoryPath, $"{ApplicationData.SqliteName}.db3");
            if (!File.Exists(dbPath))
                File.Create(dbPath);

            SQLitePCL.Batteries_V2.Init(); //This is to enforce encryption of the database

            var options = new SQLiteConnectionString(dbPath, false, key: SecurityConstants.SQLCipherKey.HashPassword(),
                            preKeyAction: db => db.Execute("PRAGMA cipher_default_use_hmac = OFF;PRAGMA hexkey=\"0x0102030405060708090a0b0c0d0e0f10\""),
                            postKeyAction: db => db.Execute("PRAGMA kdf_iter = 128000;"));

            //Register the Database service, which will be used to persist all data locally, taken from the remote web api
            container.RegisterInstance<Database>(new Database(new SQLiteConnection(options)));
        }
    }
}


