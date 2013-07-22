using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using DataAnnotationsExtensions.Resources;
using System.Web;
using MvcUmbracoDataAnnotations;

namespace DataAnnotationsExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileExtensionsAttribute : DataTypeAttribute
    {
        public string Extensions { get; private set; }
        private string _umbracoDictionaryKey = string.Empty;
        /// <summary>
        /// Provide the allowed file extensions, seperated via "|" (or a comma, ","), defaults to "png|jpe?g|gif" 
        /// </summary>
        public FileExtensionsAttribute(string allowedExtensions = "png,jpg,jpeg,gif")
            : base("fileextension")
        {
            Extensions = string.IsNullOrWhiteSpace(allowedExtensions) ? "png,jpg,jpeg,gif" : allowedExtensions.Replace("|", ",").Replace(" ", "");
        }

        public FileExtensionsAttribute(string umbracoDictionaryKey, string allowedExtensions = "png,jpg,jpeg,gif")
            : base("fileextension")
        {
            Extensions = string.IsNullOrWhiteSpace(allowedExtensions) ? "png,jpg,jpeg,gif" : allowedExtensions.Replace("|", ",").Replace(" ", "");
            _umbracoDictionaryKey = umbracoDictionaryKey;
        }

        public override string FormatErrorMessage(string name)
        {
            if (ErrorMessage == null && ErrorMessageResourceName == null)
            {
                ErrorMessage = ValidatorResources.FileExtensionsAttribute_Invalid;
            }
            if (_umbracoDictionaryKey != string.Empty)
            {
                ErrorMessage = Helper.GetDictionaryItem(_umbracoDictionaryKey);
            }
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Extensions);
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string valueAsString;
            if (value != null && value is HttpPostedFileBase)
            {
                valueAsString = (value as HttpPostedFileBase).FileName;
            }
            else
            {
                valueAsString = value as string;
            }

            if (valueAsString != null)
            {
                return ValidateExtension(valueAsString);
            }

            return false;
        }

        private bool ValidateExtension(string fileName)
        {
            try
            {
                return Extensions.Split(',').Contains(Path.GetExtension(fileName).Replace(".","").ToLowerInvariant());
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}