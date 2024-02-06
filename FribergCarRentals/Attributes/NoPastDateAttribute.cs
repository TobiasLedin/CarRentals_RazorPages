using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FribergCarRentals.Attributes
{
    public class NoPastDateAttribute : ValidationAttribute, IClientModelValidator
    {
        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if (dt < DateTime.Now.Date)
            {
                return false;
            }

            return true;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-nopastdate", "Date cannot be in the past");
        }
    }
}
