using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ServiceProvider.DO
{
    public class DO_DoctorBusinessLink
    {
        public int BusinessKey { get; set; }
        public string? BusinessLocation { get; set; }
        public int DoctorId { get; set; }
        public int TimeSlotInMins { get; set; }
        public int PatientCountPerHour { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public int IsdCode { get; set; }

    }
}
