using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ServiceProvider.DO
{
    public class DO_DoctorLeave
    {
        public int BusinessKey { get; set; }
        public int DoctorId { get; set; }
        public DateTime OnLeaveFrom { get; set; }
        public DateTime OnLeaveTill { get; set; }
        public int NoOfDays { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
    }
}
