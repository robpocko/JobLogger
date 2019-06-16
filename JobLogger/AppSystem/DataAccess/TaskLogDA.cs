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
    internal class TaskLogAPI
    {
        [DataMember]
        public long id { get; set; }
        [DataMember]
        public string logDate { get; set; }
        [IgnoreDataMember]
        public DateTime logDateInternal { get; set; }
        [DataMember]
        public string startTime { get; set; }
        [IgnoreDataMember]
        public TimeSpan startTimeInternal { get; set; }
        [DataMember]
        public string endTime { get; set; }
        [IgnoreDataMember]
        public TimeSpan endTimeInternal { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public long? taskID { get; set; }
        [DataMember]
        public TaskAPI task { get; set; }
        [DataMember]
        public virtual ICollection<CheckInAPI> checkIns { get; set; }

        public bool IsNotNew { get { return id > 0; } }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(logDate))
                {
                    logDateInternal = DateTime.Parse(logDate);
                }
                if (!string.IsNullOrEmpty(startTime))
                {
                    startTimeInternal = TimeSpan.Parse(startTime);
                }
                if (!string.IsNullOrEmpty(endTime))
                {
                    endTimeInternal = TimeSpan.Parse(endTime);
                }
            }
            catch (FormatException)
            {

            }
        }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(TaskLogAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    internal class TaskLog
    {
        internal static async Task<TaskLogAPI> Get(long id)
        {
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = new Uri(
                    string.Format("{0}/{1}/{2}",
                    AppSettings.ServerUrl, APICommon.TASKLOG_PATH, id));

                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(TaskLogAPI));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response));

                    return (TaskLogAPI)js.ReadObject(ms);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        internal static async Task<TaskLogAPI> Save(TaskLogAPI item)
        {
            item.logDate = item.logDateInternal.ToString("dd MMM yyyy");
            TaskLogAPI result = null;

            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                string suffix = item.id > 0 ? string.Format("/{0}", item.id) : string.Empty;
                Uri uri = new Uri(
                    string.Format(
                        "{0}/{1}{2}",
                        AppSettings.ServerUrl,
                        APICommon.TASKLOG_PATH,
                        suffix));

                try
                {
                    HttpStringContent content = new HttpStringContent(item.ToJson(), Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
                    HttpResponseMessage response;
                    if (item.id <= 0)
                    {
                        response = await client.PostAsync(uri, content);
                    }
                    else
                    {
                        response = await client.PutAsync(uri, content);
                    }

                    response.EnsureSuccessStatusCode();

                    string resultStr = await response.Content.ReadAsStringAsync();

                    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(TaskLogAPI));
                    MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(resultStr));

                    result = (TaskLogAPI)js.ReadObject(ms);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }
    }
}
