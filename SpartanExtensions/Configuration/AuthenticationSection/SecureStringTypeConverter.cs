using System;
using System.ComponentModel;
using System.Globalization;

namespace SpartanExtensions.Configuration.AuthenticationSection
{
    public class SecureStringTypeConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return ((string)value).ToSecureString();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }
    }
}
