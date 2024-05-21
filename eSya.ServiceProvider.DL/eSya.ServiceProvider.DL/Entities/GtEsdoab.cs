using System;
using System.Collections.Generic;

namespace eSya.ServiceProvider.DL.Entities
{
    public partial class GtEsdoab
    {
        public int DoctorId { get; set; }
        public string LanguageKnown { get; set; } = null!;
        public string Experience { get; set; } = null!;
        public string DoctorRemarks { get; set; } = null!;
        public string CertificationCourse { get; set; } = null!;
        public string AboutDoctor { get; set; } = null!;
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }
    }
}
