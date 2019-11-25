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
    internal class CodeBranchesAPI
    {
        [DataMember]
        internal long id { get; set; }
        [DataMember]
        internal string name { get; set; }

        public override string ToString()
        {
            return name;
        }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(CodeBranchesAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    [DataContract]
    internal class CodeBranchesListAPI
    {
        [DataMember]
        internal int recordCount { get; set; }
        [DataMember]
        internal List<CodeBranchesAPI> data { get; set; }
    }

    internal class CodeBranches
    {
        internal static async Task<CodeBranchesListAPI> Get(int page, int pageSize, bool showInactive, string name = "")
        {
            CodeBranchesListAPI data = null;
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = null;

                if (name == null || name.Length == 0)
                {
                    uri = new Uri(string.Format(
                    "{0}/{1}?page={2}&pagesize={3}&showInActive={4}",
                    AppSettings.ServerUrl,
                            APICommon.CODEBRANCH_PATH,
                            page,
                            pageSize,
                            showInactive));
                }
                else
                {
                    uri = new Uri(string.Format(
                    "{0}/{1}?page={2}&pagesize={3}&name={4}&showInActive={5}",
                    AppSettings.ServerUrl,
                            APICommon.CODEBRANCH_PATH,
                            page,
                            pageSize,
                            name,
                            showInactive));
                }


                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(CodeBranchesListAPI));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response));

                    data = (CodeBranchesListAPI)js.ReadObject(ms);
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
