using System;
using System.Collections.Generic;

namespace eSya.ServiceProvider.DL.Entities
{
    public partial class GtEsspct
    {
        public int BusinessKey { get; set; }
        public int SpecialtyId { get; set; }
        public int ClinicId { get; set; }
        public int RateType { get; set; }
        public int ClinicType { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public string ServiceRule { get; set; } = null!;
        public decimal ConsultationTariff { get; set; }
        public decimal TeleConsultationTariff { get; set; }
        public decimal RevisitConsultationTariff { get; set; }
        public int RevisitRule { get; set; }
        public int SecRevisitRule { get; set; }
        public decimal SecRevisitConsultationTariff { get; set; }
        public DateTime? EffectiveTill { get; set; }
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
