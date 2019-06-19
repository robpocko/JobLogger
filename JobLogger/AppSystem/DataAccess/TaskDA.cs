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
    internal class TaskAPI
    {
        [DataMember]
        internal string id { get; set; }
        [DataMember]
        internal string title { get; set; }
        [DataMember]
        internal TaskType taskType { get; set; }
        [DataMember]
        internal bool isActive { get; set; }
        [DataMember]
        internal long? requirementID { get; set; }
        [DataMember]
        internal RequirementAPI requirement { get; set; }
        [DataMember]
        internal List<TaskCommentAPI> comments { get; set; }
        [DataMember]
        internal List<TaskLogAPI> logs { get; set; }
        [DataMember]
        internal List<TaskCheckInAPI> checkIns { get; set; }
        [DataMember]
        internal bool isNew { get; set; }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(TaskAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    internal class jlTask
    {
        internal static async Task<TaskAPI> Get(long id)
        {
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = new Uri(
                    string.Format("{0}/{1}/{2}",
                    AppSettings.ServerUrl, APICommon.TASK_PATH, id));

                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(TaskAPI));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response));

                    return (TaskAPI)js.ReadObject(ms);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        internal static async Task<TaskAPI> Save(TaskAPI item)
        {
            TaskAPI result = null;

            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                string suffix = !item.isNew ? string.Format("/{0}", item.id) : string.Empty;
                Uri uri = new Uri(
                    string.Format(
                        "{0}/{1}{2}",
                        AppSettings.ServerUrl,
                        APICommon.TASK_PATH,
                        suffix));

                try
                {
                    HttpStringContent content = new HttpStringContent(item.ToJson(), Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
                    HttpResponseMessage response;
                    if (item.isNew)
                    {
                        response = await client.PostAsync(uri, content);
                    }
                    else
                    {
                        response = await client.PutAsync(uri, content);
                    }

                    response.EnsureSuccessStatusCode();

                    string resultStr = await response.Content.ReadAsStringAsync();

                    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(TaskAPI));
                    MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(resultStr));

                    result = (TaskAPI)js.ReadObject(ms);
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
