using System;
using System.Collections.Generic;

namespace eSya.ServiceProvider.DL.Entities
{
    public partial class GtEadsfi
    {
        public int BusinessKey { get; set; }
        public string RoomNo { get; set; } = null!;
        public int FloorId { get; set; }
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
