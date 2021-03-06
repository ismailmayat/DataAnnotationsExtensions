﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DataAnnotationsExtensions.Resources;
using MvcUmbracoDataAnnotations;

namespace DataAnnotationsExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CreditCardAttribute : DataTypeAttribute
    {
        private string _umbracoDictionaryKey = string.Empty;
        public CreditCardAttribute()
            : base("creditcard")
        {
        }

        public CreditCardAttribute(string umbracoDictionaryKey):base("creditcard")
        {
            _umbracoDictionaryKey = umbracoDictionaryKey;        
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = ValidatorResources.CreditCardAttribute_Invalid;
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

            var ccValue = value as string;
            if (ccValue == null)
            {
                return false;
            }

            ccValue = ccValue.Replace("-", string.Empty).Replace(" ", string.Empty);

            if (string.IsNullOrEmpty(ccValue)) return false; //Don't accept only dashes/spaces

            int checksum = 0;
            bool evenDigit = false;

            // http://www.beachnet.com/~hstiles/cardtype.html
            foreach (char digit in ccValue.Reverse())
            {
                if (!Char.IsDigit(digit))
                {
                    return false;
                }

                int digitValue = (digit - '0') * (evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;
                    digitValue /= 10;
                }
            }

            return (checksum % 10) == 0;
        }
    }
}