using System;

using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ExtensionMethods
{
    public static class StringBuilderExtensions
    {
        private const byte _indentSize = 4;

        public static StringBuilder AddIndent(this StringBuilder originalStringBuilder,int indentLevel)
        {
            originalStringBuilder.Append("".PadLeft(indentLevel * _indentSize));

            return originalStringBuilder;
        }
        public static StringBuilder AppendLineWithIndent(this StringBuilder originalStringBuilder,string str, int indentLevel)
        {
            originalStringBuilder.Append("".PadLeft(indentLevel * _indentSize));

            originalStringBuilder.Append(str);

            originalStringBuilder.AppendLine();

            return originalStringBuilder;
        }
        public static StringBuilder AppendLineWithIndent(this StringBuilder originalStringBuilder, string str)
        {
            originalStringBuilder.Append("".PadLeft(4 * _indentSize));

            originalStringBuilder.Append(str);

            originalStringBuilder.AppendLine();

            return originalStringBuilder;
        }
        public static StringBuilder AppendLineAndIndent(this StringBuilder originalStringBuilder, int indentLevel)
        {
            originalStringBuilder.AppendLine();

            originalStringBuilder.Append("".PadLeft(indentLevel * _indentSize));

            return originalStringBuilder;
        }
        public static StringBuilder AppendLineAndIndent(this StringBuilder originalStringBuilder)
        {
            originalStringBuilder.AppendLine();

            originalStringBuilder.Append("".PadLeft(4 * _indentSize));

            return originalStringBuilder;
        }
    }
    public static class StringExtensions
    {
        private const byte _indentSize = 4;

        public static string Indent(this string originalString, int indentLevel)
        {
            StringBuilder indentedString = new StringBuilder();
            indentedString.Append("".PadLeft(indentLevel * _indentSize));
            indentedString.Append(originalString);
            return indentedString.ToString();
        }

        public static bool IsNotNullOrWhiteSpace(this object obj)
        {

            if (obj == null) return false;

            return true;
        }
        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            if (value != null) return true;
            
            return false;
        }
        public static string NullSafeToString(this object obj)
        {
            return obj != null ? obj.ToString() : String.Empty;
        }
        public static string EmptyIfNull(this string str)
        {
            if (str == null) return string.Empty;

            return str;
        }
    }
}