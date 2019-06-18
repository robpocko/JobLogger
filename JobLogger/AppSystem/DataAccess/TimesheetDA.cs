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
    [DataContract]
    internal class TimesheetAPI
    {
        [DataMember]
        public string taskStartTime { get; set; }
        [DataMember]
        public string taskEndTime { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public int taskDuration { get; set; }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(TimesheetAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    internal class Timesheet
    {
        internal static async Task<List<TimesheetAPI>> Get(DateTime reportDate)
        {
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = new Uri(
                    string.Format("{0}/{1}/TimesheetForDay?reportDate={2}",
                    AppSettings.ServerUrl, APICommon.TIMESHEET_PATH, reportDate.ToString("d MMM yyyy")));

                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(List<TimesheetAPI>));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response));

                    return (List<TimesheetAPI>)js.ReadObject(ms);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
