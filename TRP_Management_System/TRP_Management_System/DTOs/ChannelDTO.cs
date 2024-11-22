using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRP_Management_System.DTOs
{
    public class ChannelDTO
    {
        public int ChannelId { get; set; } // Primary key
        public string ChannelName { get; set; } // Max length 100, required, unique
        public int EstablishedYear { get; set; } // Range: 1900 - Current Year
        public string Country { get; set; } // Max length 50, required  


    }
}