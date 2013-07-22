using System;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.Resources;
using MvcUmbracoDataAnnotations;

namespace DataAnnotationsExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NumericAttribute : DataTypeAttribute
    {
        private string _umbracoDictionaryKey = string.Empty;
        public NumericAttribute() : base("numeric")
        {
        }

        public NumericAttribute(string umbracoDictionaryKey)
            : base("numeric")
        {
            _umbracoDictionaryKey = umbracoDictionaryKey;
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = ValidatorResources.NumericAttribute_Invalid;
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

            double retNum;

            return double.TryParse(Convert.ToString(value), out retNum);
        }
    }
}
