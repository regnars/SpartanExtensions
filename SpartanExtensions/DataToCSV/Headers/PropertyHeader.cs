using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SpartanExtensions
{
    internal class PropertyHeader<T> : HeaderBase<T>
    {
        private PropertyInfo _propertyInfo;        

        public PropertyHeader(PropertyInfo propertyInfo)
            :base(propertyInfo.Name)
        {
            this._propertyInfo = propertyInfo;
        }

        internal override object GetValue(T dataItem)
        {
            return _propertyInfo.GetValue(dataItem, null);
        }
    }
}
