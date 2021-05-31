using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contracts.Validators
{
    public class PhoneEmailValidator : ValidationAttribute
    {
        private const string DefaultErrorMessageFormatString = "The {0} field is required.";
        private readonly string[] _dependentProperties;

        public PhoneEmailValidator(string[] dependentProperties)
        {
            _dependentProperties = dependentProperties;
            ErrorMessage = DefaultErrorMessageFormatString;
        }
        protected override ValidationResult IsValid(Object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();

            foreach (string s in _dependentProperties)
            {
                Object propertyValue = type.GetProperty(s).GetValue(instance, null);
                if (
                    ((string)propertyValue != ""
                    && propertyValue !=null) ||
                    ((string)value != ""
                    && value != null)
                    )
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(context.DisplayName + " required. ");
        }
    }
}
