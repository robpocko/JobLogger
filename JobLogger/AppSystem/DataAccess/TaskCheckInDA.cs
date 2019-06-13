using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace JobLogger.AppSystem.DataAccess
{
    [DataContract]
    internal class TaskCheckInAPI
    {
        [DataMember]
        internal long taskID { get; set; }
        [DataMember]
        internal long checkInID { get; set; }

        internal string ToJson()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer =
                    new DataContractJsonSerializer(typeof(TaskCheckInAPI));

                serializer.WriteObject(stream, this);

                stream.Position = 0;

                StreamReader sr = new StreamReader(stream);

                return sr.ReadToEnd();
            }
        }
    }
}
