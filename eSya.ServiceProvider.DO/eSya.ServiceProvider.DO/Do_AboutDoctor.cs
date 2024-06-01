using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ServiceProvider.DO
{
    public class Do_AboutDoctor
    {
        public int DoctorId { get; set; }
        public string LanguageKnown { get; set; }
        public string Experience { get; set; }
        public string CertificationCourse { get; set; }
        public string AboutDoctor { get; set; }
        public string DoctorRemarks { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }

    }
}
