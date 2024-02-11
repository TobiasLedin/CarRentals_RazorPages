using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.Attributes
{
    public class EqualOrGreaterAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _bookingStart;

        public EqualOrGreaterAttribute(string bookingStart)
        {
            _bookingStart = bookingStart;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;
            var comparisonValue = (DateTime)validationContext.ObjectType.GetProperty(_bookingStart).GetValue(validationContext.ObjectInstance);

            if (currentValue < comparisonValue)
            {
                return new ValidationResult(ErrorMessage = "Cannot be less than Pickup date");
            }

            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-equalorgreater", "Cannot be less than Pickup date");
            context.Attributes.Add("data-val-equalorgreater-bookingstart", _bookingStart);
        }
    }
}
