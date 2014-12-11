using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace SpartanExtensions.DataToCSV
{
    public class DataToCsv<T>
    {
        private const string OffsetValue = ";";

        /// <summary>
        /// Memory stream containing csv file.
        /// </summary>
        public MemoryStream Csv { get; private set; }

        /// <summary>
        /// Converts IEnumerable of type T to csv file. Content is in "Csv" property after the initialization of this object. 
        /// </summary>
        /// <param name="data">Data to write to csv</param>
        /// <param name="customHeaderRows">Collection of header rows for csv file that has nothing to do with the data.</param>
        /// <param name="customHeaders">Collection of headers for csv file.</param>
        public DataToCsv(IEnumerable<T> data, IEnumerable<string> customHeaderRows = null,
            params HeaderBase<T>[] customHeaders)
        {
            Csv = GetCsv(data, customHeaders, customHeaderRows);
        }

        private MemoryStream GetCsv(IEnumerable<T> data,
            IEnumerable<HeaderBase<T>> headers, IEnumerable<string> customHeaderRows = null)
        {
                
            // ReSharper disable PossibleMultipleEnumeration
            if (data == null || !data.Any())
            {
                return new MemoryStream();
            }

            if (headers == null || !headers.Any())
            {
                headers = CreateHeadersFromProperties<T>(typeof(T));
            }

            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream, Encoding.UTF8);
            WriteCustomHeaderRows(streamWriter, customHeaderRows);
            WriteHeader(streamWriter, headers);
            WriteContent(data, streamWriter, headers);
            streamWriter.Flush();
            stream.Position = 0;
            return stream;
            // ReSharper restore PossibleMultipleEnumeration
        }

        private void WriteCustomHeaderRows(StreamWriter streamWriter, IEnumerable<string> customHeaderRows)
        {
            foreach (var headerRow in customHeaderRows)
            {
                streamWriter.Write(headerRow);
                streamWriter.Write(Environment.NewLine);
            }
        }

        private IEnumerable<HeaderBase<TD>> CreateHeadersFromProperties<TD>(Type type)
        {
            var properties = type.GetProperties();
            return properties
                .Where(x =>
                    x.Name != "Id"
                    && IsBrowsable(x))
                .Select(x => new PropertyHeader<TD>(x));
        }

        private bool IsBrowsable(PropertyInfo property)
        {
            object attribute = property.GetCustomAttributes(inherit: false).FirstOrDefault(x => x is BrowsableAttribute);
            if (attribute == null)
                return true;
            var browsableAttribute = attribute as BrowsableAttribute;
            return browsableAttribute != null && browsableAttribute.Browsable;
        }

        private void WriteHeader(StreamWriter streamWriter,
            IEnumerable<HeaderBase<T>> customHeaders)
        {
            foreach (HeaderBase<T> header in customHeaders)
            {
                streamWriter.Write(header.Header);
                streamWriter.Write(OffsetValue);
            }
            streamWriter.Write(Environment.NewLine);
        }

        private void WriteContent(IEnumerable<T> data, StreamWriter streamWriter, IEnumerable<HeaderBase<T>> headers)
        {
            foreach (var item in data)
            {
                var offset = 0;
                // ReSharper disable once PossibleMultipleEnumeration
                foreach (var header in headers)
                {
                    WriteValue(item, header, offset, streamWriter);
                    offset++;
                }
                streamWriter.Write(Environment.NewLine);
            }
        }

        private void WriteValue<TItem>(TItem item, HeaderBase<TItem> header,
            int offset, StreamWriter streamWriter)
        {
            var value = header.GetValue(item);
            if (value == null)
            {
                streamWriter.Write(OffsetValue);
            }
            else if (value is bool)
            {
                streamWriter.Write((bool)value ? "Yes" : "No");
                streamWriter.Write(OffsetValue);
            }
            else if (value is DateTime)
            {
                var dateFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
                streamWriter.Write(((DateTime) value).ToString("d", dateFormat));
                streamWriter.Write(OffsetValue);
            }
            else if (value is ICollection)
            {
                // ReSharper disable PossibleMultipleEnumeration
                streamWriter.Write(OffsetValue);
                IEnumerable<HeaderBase<object>> childHeaders = null;
                foreach (var itemValue in value as ICollection)
                {
                    if (itemValue == null)
                    {
                        continue;
                    }
                    streamWriter.Write(Environment.NewLine);
                    Offset(offset, streamWriter);
                    if (childHeaders == null)
                    {
                        childHeaders = CreateHeadersFromProperties<object>(itemValue.GetType());
                    }
                    foreach (var itemHeader in childHeaders)
                    {
                        WriteValue(itemValue, itemHeader, offset, streamWriter);
                    }
                }
                // ReSharper restore PossibleMultipleEnumeration
            }
            else if (value is string)
            {
                var strVal = value as string;
                var wrappedInQuotMarks = strVal.StartsWith("\"") && strVal.EndsWith("\"");
                if (wrappedInQuotMarks)
                    streamWriter.Write("\"\"");
                streamWriter.Write(value);
                if (wrappedInQuotMarks)
                    streamWriter.Write("\"\"");
                streamWriter.Write(OffsetValue);
            }
            else if (value is Enum)
            {
                //do nothing
            }
            else
            {
                streamWriter.Write(value.ToString());
                streamWriter.Write(OffsetValue);
            }
        }

        private void Offset(int offset, StreamWriter streamWriter)
        {
            for (var index = 1; index <= offset; index++)
            {
                streamWriter.Write(OffsetValue);
            }
        }
    }
}