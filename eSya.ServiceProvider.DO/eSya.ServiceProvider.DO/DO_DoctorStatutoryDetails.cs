using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ServiceProvider.DO
{
    public class DO_DoctorStatutoryDetails
    {
        public int DoctorId { get; set; }
        public int Isdcode { get; set; }
        public int StatutoryCode { get; set; }
        public string StatutoryDescription { get; set; }
        public decimal TaxPerc { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTill { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormID { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public string StatutoryValue { get; set; }
    }
    public class DO_ISDCodes
    {
        public int Isdcode { get; set; }
        public string MobileNumberPattern { get; set; }
        public string CountryFlag { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string DomainName { get; set; }
    }
}
