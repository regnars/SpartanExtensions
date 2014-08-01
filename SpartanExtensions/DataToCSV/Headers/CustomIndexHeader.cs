using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace SpartanExtensions
{
    public class CustomIndexHeader<T>:HeaderBase<T>
    {
        private Func<T, int, object> _fieldByIndexSelector;        
        private int _fieldIndex;

        public CustomIndexHeader(string header, int fieldIndex, Func<T, int, object> fieldSelector)
            :base(header)
        {
            _fieldByIndexSelector = fieldSelector;
            _fieldIndex = fieldIndex;
        }

        internal override object GetValue(T dataItem)
        {
            return _fieldByIndexSelector(dataItem, _fieldIndex);
        }
    }
}
