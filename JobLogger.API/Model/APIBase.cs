using JobLogger.DAL;
using System;

namespace JobLogger.API.Model
{
    public abstract class APIBase
    {
        public long ID { get; set; }
        public bool IsNew { get; set; }

        public static EFBase To(APIBase item)
        {
            throw new NotImplementedException("You must override this method in the subclass");
        }

        public static APIBase From(EFBase item)
        {
            throw new NotImplementedException("You must override this method in the subclass");
        }
    }
}
