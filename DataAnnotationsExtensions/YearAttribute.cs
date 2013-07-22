using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using DataAnnotationsExtensions.Resources;
using MvcUmbracoDataAnnotations;

namespace DataAnnotationsExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class YearAttribute : DataTypeAttribute
    {
        private static Regex _regex = new Regex(@"^[0-9]{4}$");
        private string _umbracoDictionaryKey = string.Empty;
        public string Regex
        {
            get
            {
                return _regex.ToString();
            }
        }

        public YearAttribute()
            : base("year")
        {

        }

        public YearAttribute(string umbracoDictionaryKey)
            : base("year")
        {
           _umbracoDictionaryKey= umbracoDictionaryKey ;
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = ValidatorResources.YearAttribute_Invalid;
            }
            if (_umbracoDictionaryKey != string.Empty)
            {
                ErrorMessage = Helper.GetDictionaryItem(_umbracoDictionaryKey);
            }
            return base.FormatErrorMessage(name);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            int retNum;
            var parseSuccess = int.TryParse(Convert.ToString(value), out retNum);

            return parseSuccess && retNum >= 1 && retNum <= 9999;
        }
    }
}
