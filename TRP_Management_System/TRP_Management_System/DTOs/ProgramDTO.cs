using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TRP_Management_System.EF;

namespace TRP_Management_System.DTOs
{
    public class ProgramDTO
    {
        public int ProgramId { get; set; } // Primary key, auto-incremented, no validation needed.

        [Required(ErrorMessage = "Program Name is required.")]
        [StringLength(150, ErrorMessage = "Program Name cannot exceed 150 characters.")]
        public string ProgramName { get; set; }

        [Required(ErrorMessage = "TRP Score is required.")]
        [Range(0.0, 10.0, ErrorMessage = "TRP Score must be between 0.0 and 10.0.")]
        public decimal TRPScore { get; set; }

        [Required(ErrorMessage = "ChannelId is required.")]
        [ValidChannelId]
        public int ChannelId { get; set; }

        [Required(ErrorMessage = "Air Time is required.")]
        public TimeSpan AirTime { get; set; }
    }


    // Custom validation attribute for ChannelId
    public class ValidChannelIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (TRP_DBEntities1)validationContext.GetService(typeof(TRP_DBEntities1));
            if (value is int channelId && !dbContext.Channels.Any(c => c.ChannelId == channelId))
            {
                return new ValidationResult("The selected Channel ID does not exist.");
            }
            return ValidationResult.Success;
        }
    }
}