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
    internal class CheckInAPI
    {
        [DataMember]
        internal string id { get; set; }
        [DataMember]
        internal string comment { get; set; }
        [DataMember]
        internal string checkInTime { get; set; }
        [IgnoreDataMember]
        internal DateTime checkInTimeInternal { get; set; }
        [DataMember]
        internal long? taskLogID { get; set; }
        [DataMember]
        internal TaskLogAPI taskLog { get; set; }
        [DataMember]
        internal long codeBranchID { get; set; }
        [DataMember]
        internal CodeBranchAPI codeBranch { get; set; }
        [DataMember]
        internal List<TaskCheckInAPI> taskCheckIns { get; set; }
        [DataMember]
        internal bool isNew { get; set; }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(checkInTime))
                {
                    checkInTimeInternal = DateTime.Parse(checkInTime);
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
                    new DataContractJsonSerializer(typeof(CheckInAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    internal class CheckIn
    {
        internal static async Task<CheckInAPI> Get(long id)
        {
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = new Uri(
                    string.Format("{0}/{1}/{2}",
                    AppSettings.ServerUrl, APICommon.CHECKIN_PATH, id));

                //  http://localhost:24227/api/CheckIn/79984
                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(CheckInAPI));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response));

                    return (CheckInAPI)js.ReadObject(ms);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        internal static async Task<CheckInAPI> Save(CheckInAPI item)
        {
            item.checkInTime = item.checkInTimeInternal.ToString("dd MMM yyyy HH:mm");
            CheckInAPI result = null;

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
                        APICommon.CHECKIN_PATH,
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

                    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(CheckInAPI));
                    MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(resultStr));

                    result = (CheckInAPI)js.ReadObject(ms);
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
