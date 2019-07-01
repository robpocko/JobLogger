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
    internal class RequirementAPI
    {
        [DataMember]
        internal string id { get; set; }
        [DataMember]
        internal string title { get; set; }
        [DataMember]
        internal RequirementStatus status { get; set; }
        [DataMember]
        internal long? featureID { get; set; }
        [DataMember]
        internal FeatureAPI feature { get; set; }
        [DataMember]
        internal List<RequirementCommentAPI> comments { get; set; }
        [DataMember]
        internal List<TaskAPI> tasks { get; set; }
        [DataMember]
        internal bool isNew { get; set; }
        internal bool isNotNew { get { return !isNew; } }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(RequirementAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    internal class Requirement
    {
        internal static async Task<RequirementAPI> Get(long id)
        {
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = new Uri(
                    string.Format("{0}/{1}/{2}",
                    AppSettings.ServerUrl, APICommon.REQUIREMENT_PATH, id));

                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(RequirementAPI));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response)); 

                    return (RequirementAPI)js.ReadObject(ms);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        internal static async Task<RequirementAPI> Save(RequirementAPI item)
        {
            RequirementAPI result = null;

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
                        APICommon.REQUIREMENT_PATH,
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

                    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(RequirementAPI));
                    MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(resultStr));

                    result = (RequirementAPI)js.ReadObject(ms);
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
