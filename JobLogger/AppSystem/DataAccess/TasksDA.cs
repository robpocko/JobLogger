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
    internal class TasksAPI
    {
        [DataMember]
        internal long id { get; set; }
        [DataMember]
        internal string title { get; set; }
        [DataMember]
        internal TaskType taskType { get; set; }

        public override string ToString()
        {
            return title;
        }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(TasksAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    [DataContract]
    internal class TasksListAPI
    {
        [DataMember]
        internal int recordCount { get; set; }
        [DataMember]
        internal List<TasksAPI> data { get; set; }
    }

    internal class Tasks
    {
        internal static async Task<TasksListAPI> Get(
            int page, int pageSize, bool showInActive, string title = "", TaskType? taskType = null)
        {
            TasksListAPI data = null;
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = null;

                if ( title == null || title.Length == 0)
                {
                    if (!taskType.HasValue)
                    {
                        uri = new Uri(string.Format(
                            "{0}/{1}?page={2}&pagesize={3}&showInActive={4}",
                            AppSettings.ServerUrl,
                            APICommon.TASK_PATH,
                            page,
                            pageSize,
                            showInActive));
                    }
                    else
                    {
                        uri = new Uri(string.Format(
                            "{0}/{1}?page={2}&pagesize={3}&showInActive={4}&taskType={5}",
                            AppSettings.ServerUrl,
                            APICommon.TASK_PATH,
                            page,
                            pageSize,
                            showInActive,
                            taskType.Value));
                    }
                }
                else
                {
                    if (!taskType.HasValue)
                    {
                        uri = new Uri(string.Format(
                            "{0}/{1}?page={2}&pagesize={3}&showInActive={4}&title={5}",
                            AppSettings.ServerUrl,
                            APICommon.TASK_PATH,
                            page,
                            pageSize,
                            showInActive,
                            title));
                    }
                    else
                    {
                        uri = new Uri(string.Format(
                            "{0}/{1}?page={2}&pagesize={3}&showInActive={4}&title={5}&taskType={6}",
                            AppSettings.ServerUrl,
                            APICommon.TASK_PATH,
                            page,
                            pageSize,
                            showInActive,
                            title,
                            taskType.Value));
                    }
                }

                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(TasksListAPI));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response));

                    data = (TasksListAPI)js.ReadObject(ms);
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
