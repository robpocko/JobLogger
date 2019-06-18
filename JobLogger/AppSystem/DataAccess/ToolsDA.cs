using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace JobLogger.AppSystem.DataAccess
{
    internal class Tools
    {
        internal static async Task BackupDatabase()
        {
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = new Uri(
                    string.Format("{0}/{1}/",
                    AppSettings.ServerUrl, APICommon.TOOLS_PATH));

                try
                {
                    var response = await client.PostAsync(uri, null);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
