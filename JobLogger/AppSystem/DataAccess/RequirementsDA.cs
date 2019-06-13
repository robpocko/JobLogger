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
    public class RequirementsAPI
    {
        [DataMember]
        internal long id { get; set; }
        [DataMember]
        internal string title { get; set; }
        [DataMember]
        internal RequirementStatus status { get; set; }

        public override string ToString()
        {
            return title;
        }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(RequirementsAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    [DataContract]
    internal class RequirementsListAPI
    {
        [DataMember]
        internal int                    recordCount { get; set; }
        [DataMember]
        internal List<RequirementsAPI>  data { get; set; }
    }

    internal class Requirements
    {
        internal static async Task<RequirementsListAPI> Get(
            int page, int pageSize, string title = "", RequirementStatus? status = null)
        {
            RequirementsListAPI data = null;
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = null;

                if (title == null || title.Length == 0)
                {
                    if (!status.HasValue)
                    {
                        uri = new Uri(string.Format(
                            "{0}/{1}?page={2}&pagesize={3}",
                            AppSettings.ServerUrl,
                            APICommon.REQUIREMENT_PATH,
                            page,
                            pageSize));
                    }
                    else
                    {
                        uri = new Uri(string.Format(
                            "{0}/{1}?page={2}&pagesize={3}&status={4}",
                            AppSettings.ServerUrl,
                            APICommon.REQUIREMENT_PATH,
                            page,
                            pageSize,
                            status));
                    }
                }
                else
                {
                    if (!status.HasValue)
                    {
                        uri = new Uri(string.Format(
                            "{0}/{1}?page={2}&pagesize={3}&title={4}",
                            AppSettings.ServerUrl,
                            APICommon.REQUIREMENT_PATH,
                            page,
                            pageSize,
                            title));
                    }
                    else
                    {
                        uri = new Uri(string.Format(
                            "{0}/{1}?page={2}&pagesize={3}&title={4}&status={5}",
                            AppSettings.ServerUrl,
                            APICommon.REQUIREMENT_PATH,
                            page,
                            pageSize,
                            title,
                            status));
                    }
                }

                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(RequirementsListAPI));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response));

                    data = (RequirementsListAPI)js.ReadObject(ms);
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
