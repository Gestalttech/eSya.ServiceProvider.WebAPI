using System;
using System.Collections.Generic;

namespace eSya.ServiceProvider.DL.Entities
{
    public partial class GtEsdold
    {
        public int BusinessKey { get; set; }
        public int DoctorId { get; set; }
        public DateTime OnLeaveFrom { get; set; }
        public DateTime OnLeaveTill { get; set; }
        public int NoOfDays { get; set; }
        public string? Comments { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }

        public virtual GtEsdocd Doctor { get; set; } = null!;
    }
}
