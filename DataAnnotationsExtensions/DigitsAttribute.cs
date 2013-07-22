using System;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.Resources;
using MvcUmbracoDataAnnotations;

namespace DataAnnotationsExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DigitsAttribute : DataTypeAttribute
    {
        private string _umbracoDictionaryKey = string.Empty;
        public DigitsAttribute()
            : base("digits")
        {
        }

        public DigitsAttribute(string umbracoDictionaryKey)
            : base("digits")
        {
            _umbracoDictionaryKey = umbracoDictionaryKey;
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = ValidatorResources.DigitsAttribute_Invalid;
            }
            if (_umbracoDictionaryKey != string.Empty)
            {
                ErrorMessage = Helper.GetDictionaryItem(_umbracoDictionaryKey);
            }
            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            long retNum;

            var parseSuccess = long.TryParse(Convert.ToString(value), out retNum);

            return parseSuccess && retNum >= 0;
        }
    }
}