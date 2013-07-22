using System;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.Resources;
using MvcUmbracoDataAnnotations;

namespace DataAnnotationsExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IntegerAttribute : DataTypeAttribute
    {
        private string _umbracoDictionaryKey = string.Empty;
        public IntegerAttribute()
            : base("integer")
        {
        }

        public IntegerAttribute(string umbracoDictionaryKey)
            : base("integer")
        {
            _umbracoDictionaryKey = umbracoDictionaryKey;
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = ValidatorResources.IntegerAttribute_Invalid;
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

            int retNum;

            return int.TryParse(Convert.ToString(value), out retNum);
        }
    }
}