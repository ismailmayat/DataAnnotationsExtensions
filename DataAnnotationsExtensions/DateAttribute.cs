using System;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions.Resources;
using MvcUmbracoDataAnnotations;

namespace DataAnnotationsExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateAttribute : DataTypeAttribute
    {
        private string _umbracoDictionaryKey = string.Empty;
        public DateAttribute()
            : base(DataType.Date)
        {
        }

        public DateAttribute(string umbracoDictionaryKey)
            : base(DataType.Date)
        {
            _umbracoDictionaryKey = umbracoDictionaryKey;
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = ValidatorResources.DateAttribute_Invalid;
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

            DateTime retDate;

            return DateTime.TryParse(Convert.ToString(value), out retDate);
        }
    }
}