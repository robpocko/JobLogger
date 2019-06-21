//using System.IO;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;

//namespace JobLogger.AppSystem.DataAccess
//{
//    [DataContract]
//    internal class TaskCommentAPI
//    {
//        [DataMember]
//        internal long id { get; set; }
//        [DataMember]
//        internal string comment { get; set; }
//        [DataMember]
//        internal TaskAPI task { get; set; }

//        internal string ToJson()
//        {
//            using (MemoryStream stream = new MemoryStream())
//            {
//                DataContractJsonSerializer serializer =
//                    new DataContractJsonSerializer(typeof(TaskCommentAPI));

//                serializer.WriteObject(stream, this);

//                stream.Position = 0;

//                StreamReader sr = new StreamReader(stream);

//                return sr.ReadToEnd();
//            }
//        }
//    }
//}
