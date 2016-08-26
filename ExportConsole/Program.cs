using System;
using System.Threading.Tasks;
using Common.Logging;
using Devpro.EmbyData.ClientApiLib;

namespace Devpro.EmbyData.ExportConsole
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        static int Main(string[] args)
        {
            try
            {
                //TODO: check arguments and add print usage if needed
                var hostname = args[0];
                var apiKey   = args[1];
                var task     = RunGetItemsCountTask(hostname, apiKey);
                task.Wait();
                return 0;
            }
            catch (Exception exc)
            {
                Logger.ErrorFormat("An exception has been raised: \"{0}\"", exc.Message);
                Logger.DebugFormat("Stacktrace: \"{0}\"", exc.StackTrace);
                while (exc.InnerException != null)
                {
                    exc = exc.InnerException;
                    Logger.WarnFormat("Inner exception: \"{0}\"", exc.Message);
                    Logger.DebugFormat("Stacktrace: \"{0}\"", exc.StackTrace);
                }
                Console.WriteLine("An error occured: {0}", exc.Message);
                return -1;
            }
        }

        /// <summary>
        /// Create and run getItemsCount task.
        /// </summary>
        /// <param name="hostname">Emby host name url, for example "http://localhost:8096"</param>
        /// <param name="apiKey">Emby Api Key, this is defined in Expert > Advanced > Security in Emby dashboard</param>
        /// <returns></returns>
        public async static Task RunGetItemsCountTask(string hostname, string apiKey)
        {
            var client = new EmbyApiClient
            {
                Hostname = hostname,
                ApiKey   = apiKey
            };
            var resultDto = await client.GetItemsCount();
            Console.WriteLine(string.Format("getItemsCount result: MovieCount={0}", resultDto.MovieCount));
        }
    }
}
