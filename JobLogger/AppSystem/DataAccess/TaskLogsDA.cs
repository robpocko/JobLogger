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
    internal class TaskLogsAPI
    {
        [DataMember]
        internal long id { get; set; }
        [DataMember]
        internal string description { get; set; }
        [DataMember]
        internal string logDate { get; set; }
        [IgnoreDataMember]
        public string logDateInternal { get; set; }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(logDate))
                {
                    logDateInternal = DateTime.Parse(logDate).ToString("dd/MM/yy");
                }
            }
            catch (FormatException)
            {

            }
        }

        public override string ToString()
        {
            return description;
        }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(TaskLogsAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    [DataContract]
    internal class TaskLogsListAPI
    {
        [DataMember]
        internal int recordCount { get; set; }
        [DataMember]
        internal List<TaskLogsAPI> data { get; set; }
    }

    internal class TaskLogs
    {
        internal static async Task<TaskLogsListAPI> Get(int page, int pageSize)
        {
            TaskLogsListAPI data = null;
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = new Uri(string.Format(
                            "{0}/{1}?page={2}&pagesize={3}",
                            AppSettings.ServerUrl,
                            APICommon.TASKLOG_PATH,
                            page,
                            pageSize));

                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(TaskLogsListAPI));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response));

                    data = (TaskLogsListAPI)js.ReadObject(ms);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return data;
            }
        }
    }
}
