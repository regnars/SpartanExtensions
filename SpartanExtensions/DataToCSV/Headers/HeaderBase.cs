using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpartanExtensions
{
    public abstract class HeaderBase<T>
    {
        private string _header;

        public string Header
        {
            get { return _header; }
        }

        public HeaderBase(string header)
        {
            _header = header;
        }

        internal abstract object GetValue(T dataItem);
    }
}
