using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace SpartanExtensions
{
    public class CustomHeader<T>:HeaderBase<T>
    {
        private Func<T, object> _fieldSelector;

        public CustomHeader(string header, Func<T, object> fieldSelector)
           : base(header)
        {
            _fieldSelector = fieldSelector;
        }       

        internal override object GetValue(T dataItem)
        {
            return _fieldSelector(dataItem);
        }
    }
}
