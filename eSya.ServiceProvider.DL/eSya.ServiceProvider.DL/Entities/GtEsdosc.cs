﻿using System;
using System.Collections.Generic;

namespace eSya.ServiceProvider.DL.Entities
{
    public partial class GtEsdosc
    {
        public int BusinessKey { get; set; }
        public int ConsultationId { get; set; }
        public int ClinicId { get; set; }
        public int SpecialtyId { get; set; }
        public int DoctorId { get; set; }
        public DateTime ScheduleChangeDate { get; set; }
        public int SerialNo { get; set; }
        public TimeSpan ScheduleFromTime { get; set; }
        public TimeSpan ScheduleToTime { get; set; }
        public int PatientCountPerHour { get; set; }
        public int TimeSlotInMins { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; } = null!;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedTerminal { get; set; }

        public virtual GtEsdocd Doctor { get; set; } = null!;
        public virtual GtEsspcd Specialty { get; set; } = null!;
    }
}
