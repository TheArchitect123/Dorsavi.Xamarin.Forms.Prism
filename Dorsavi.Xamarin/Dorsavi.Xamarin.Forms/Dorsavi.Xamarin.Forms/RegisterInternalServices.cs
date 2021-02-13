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
    using global::Xamarin.Essentials;

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
        public static void RegisterSqliteDatabase(ref IContainerRegistry container)
        {
            //Initialize the Db directory, including access  
            string directoryPath = Path.Combine(FileSystem.AppDataDirectory, ApplicationData.DirectoryName);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string dbPath = Path.Combine(directoryPath, $"{ApplicationData.SqliteName}.db3");
            if (!File.Exists(dbPath))
                File.Create(dbPath).Dispose();

            container.RegisterInstance<Database>(new Database(new SQLiteConnection(dbPath, false)));
        }
    }
}


