using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLogger.DAL
{
    [Table("TaskCheckIn")]
    public class TaskCheckIn
    {
        public long TaskID { get; set; }

         public long CheckInID { get; set; }
    }
}
