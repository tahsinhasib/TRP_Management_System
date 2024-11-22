using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRP_Management_System.DTOs
{
    public class ProgramDTO
    {
        public int ProgramId { get; set; } // Primary Key
        public string ProgramName { get; set; } // Max Length 150, Required, Unique within Channel
        public decimal TRPScore { get; set; } // Range: 0.0 - 10.0, Required
        public int ChannelId { get; set; } // Foreign Key, Required
        public TimeSpan AirTime { get; set; } // Required
    }
}