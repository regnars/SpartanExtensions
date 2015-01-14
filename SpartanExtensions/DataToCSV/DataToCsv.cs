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
        private string OffsetValue { get; set; }
        private bool UseOffsetBeforeNewLine { get; set; }

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
        /// <param name="offsetValue">Csv offset character value, default is ";".</param>
        /// <param name="useOffsetBeforeNewLine">Should use offset value before adding newline.</param>
        public DataToCsv(IEnumerable<T> data, IEnumerable<HeaderBase<T>> customHeaders,
            IEnumerable<string> customHeaderRows = null, string offsetValue = ";",
            bool useOffsetBeforeNewLine = true)
        {
            OffsetValue = offsetValue;
            UseOffsetBeforeNewLine = useOffsetBeforeNewLine;
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

            if (customHeaderRows != null && customHeaderRows.Any())
            {
                WriteCustomHeaderRows(streamWriter, customHeaderRows);
            }

            if (headers != null && headers.Any())
            {
                WriteHeader(streamWriter, headers);

                if (data.Any())
                {
                    WriteContent(data, streamWriter, headers);
                }
            }

            streamWriter.Flush();
            stream.Position = 0;
            return stream;
            // ReSharper restore PossibleMultipleEnumeration
        }

        private static void WriteCustomHeaderRows(StreamWriter streamWriter, IEnumerable<string> customHeaderRows)
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
            var attribute = property.GetCustomAttributes(inherit: false).FirstOrDefault(x => x is BrowsableAttribute);
            if (attribute == null)
                return true;
            var browsableAttribute = attribute as BrowsableAttribute;
            return browsableAttribute != null && browsableAttribute.Browsable;
        }

        private void WriteHeader(StreamWriter streamWriter,
            IEnumerable<HeaderBase<T>> customHeaders)
        {
            // ReSharper disable PossibleMultipleEnumeration
            foreach (var header in customHeaders)
            {
                streamWriter.Write(header.Header);

                if (UseOffsetBeforeNewLine
                    || !(!UseOffsetBeforeNewLine
                         && customHeaders.Last().Equals(header)))
                    streamWriter.Write(OffsetValue);
            }
            streamWriter.Write(Environment.NewLine);
            // ReSharper restore PossibleMultipleEnumeration
        }

        private void WriteContent(IEnumerable<T> data, StreamWriter streamWriter, IEnumerable<HeaderBase<T>> headers)
        {
            foreach (var item in data)
            {
                var offset = 0;
                // ReSharper disable PossibleMultipleEnumeration
                foreach (var header in headers)
                {
                    WriteValue(item, header, offset, streamWriter,
                        shouldWriteOffsetValue: UseOffsetBeforeNewLine || !(!UseOffsetBeforeNewLine && headers.Last().Equals(header)));
                    offset++;
                }
                // ReSharper restire PossibleMultipleEnumeration
                streamWriter.Write(Environment.NewLine);
            }
        }

        private void WriteValue<TItem>(TItem item, HeaderBase<TItem> header,
            int offset, StreamWriter streamWriter, bool shouldWriteOffsetValue)
        {
            var value = header.GetValue(item);
            if (value == null)
            {
                if (shouldWriteOffsetValue)
                    streamWriter.Write(OffsetValue);
            }
            else if (value is bool)
            {
                streamWriter.Write((bool)value ? "Yes" : "No");
                if (shouldWriteOffsetValue)
                    streamWriter.Write(OffsetValue);
            }
            else if (value is DateTime)
            {
                var dateFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
                streamWriter.Write(((DateTime)value).ToString("d", dateFormat));
                if (shouldWriteOffsetValue)
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
                        WriteValue(itemValue, itemHeader, offset, streamWriter,
                            shouldWriteOffsetValue: UseOffsetBeforeNewLine || !(!UseOffsetBeforeNewLine && childHeaders.Last().Equals(itemHeader)));
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
                if (shouldWriteOffsetValue)
                    streamWriter.Write(OffsetValue);
            }
            else if (value is Enum)
            {
                //do nothing
            }
            else
            {
                streamWriter.Write(value.ToString());
                if (shouldWriteOffsetValue)
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