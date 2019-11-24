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
    internal class CodeBranchAPI
    {
        [DataMember]
        internal long id { get; set; }
        [DataMember]
        internal string name { get; set; }
        [DataMember]
        internal bool isActive { get; set; }
        [DataMember]
        internal List<CheckInAPI> branchCheckIns { get; set; }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(CodeBranchAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    internal class CodeBranch
    {
        internal static async Task<CodeBranchAPI> Get(long id)
        {
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = new Uri(
                    string.Format("{0}/{1}/{2}",
                    AppSettings.ServerUrl, APICommon.CODEBRANCH_PATH, id));

                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(CodeBranchAPI));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response));

                    return (CodeBranchAPI)js.ReadObject(ms);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        internal static async Task<CodeBranchAPI> Save(CodeBranchAPI item)
        {
            CodeBranchAPI result = null;

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
                        APICommon.CODEBRANCH_PATH,
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

                    DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(CodeBranchAPI));
                    MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(resultStr));

                    result = (CodeBranchAPI)js.ReadObject(ms);
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
