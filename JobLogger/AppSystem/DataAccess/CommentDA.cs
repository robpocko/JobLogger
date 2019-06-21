using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace JobLogger.AppSystem.DataAccess
{
    [DataContract]
    internal abstract class CommentAPI
    {
        [DataMember]
        internal long id { get; set; }
        [DataMember]
        internal string comment { get; set; }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(CommentAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }

    [DataContract]
    internal class TaskCommentAPI : CommentAPI
    {
        [DataMember]
        internal TaskAPI task { get; set; }
    }
}
