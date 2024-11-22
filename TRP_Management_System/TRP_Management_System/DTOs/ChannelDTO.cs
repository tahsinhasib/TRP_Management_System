using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TRP_Management_System.DTOs
{
    public class ChannelDTO
    {

        public int ChannelId { get; set; }

        [Required(ErrorMessage = "Channel Name is required.")]
        [StringLength(100, ErrorMessage = "Channel Name cannot exceed 100 characters.")]
        public string ChannelName { get; set; }

        [Required(ErrorMessage = "Established Year is required.")]
        [Range(1900, int.MaxValue, ErrorMessage = "Established Year must be between 1900 and the current year.")]
        [EstablishedYearValidation]
        public int EstablishedYear { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        public string Country { get; set; }


    }


    public class EstablishedYearValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int year && year > DateTime.Now.Year)
            {
                return new ValidationResult("Established Year cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}