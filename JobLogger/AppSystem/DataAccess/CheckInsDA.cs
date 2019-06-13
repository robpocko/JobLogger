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
    internal class CheckInsAPI
    {
        [DataMember]
        internal long id { get; set; }
        [DataMember]
        internal string comment { get; set; }

        public override string ToString()
        {
            return comment;
        }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(CheckInsAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    [DataContract]
    internal class CheckInsListAPI
    {
        [DataMember]
        internal int recordCount { get; set; }
        [DataMember]
        internal List<CheckInsAPI> data { get; set; }
    }

    internal class CheckIns
    {
        internal static async Task<CheckInsListAPI> Get(
            int page, int pagesize, string comment = "", long? codeBranchID = null)
        {
            CheckInsListAPI data = null;
            HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

            RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;

            RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            using (HttpClient client = new HttpClient(RootFilter))
            {
                Uri uri = null;

                string titleParam = (comment != null && comment.Length > 0) ?
                    string.Format("&comment={0}", comment) :
                    string.Empty;
                string codeBranchIDParam = codeBranchID.HasValue ?
                    string.Format("&codeBranchID={0}", codeBranchID.Value) :
                    string.Empty;

                uri = new Uri(string.Format(
                    "{0}/{1}?page={2}&pagesize={3}{4}{5}",
                    AppSettings.ServerUrl,
                    APICommon.CHECKIN_PATH,
                    page,
                    pagesize,
                    titleParam, 
                    codeBranchIDParam));

                try
                {
                    var response = await client.GetStringAsync(uri);

                    DataContractJsonSerializer js =
                        new DataContractJsonSerializer(typeof(CheckInsListAPI));
                    MemoryStream ms =
                        new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(response));

                    data = (CheckInsListAPI)js.ReadObject(ms);
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
