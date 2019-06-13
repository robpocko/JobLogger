using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

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
}
