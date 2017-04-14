﻿using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EasyLOB.Library
{
    public static class StringExtensions
    {
        #region Base64

        /// <summary>
        /// Decode from Base64.
        /// </summary>
        /// <param name="encodedData">Encoded data string</param>
        /// <returns>Decoded data string</returns>
        public static string DecodeFromBase64(this string encodedData)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
            return ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
        }

        /// <summary>
        /// Encode to Base64.
        /// </summary>
        /// <param name="decodedData">Data string</param>
        /// <returns>Encoded data string</returns>
        public static string EncodeToBase64(this string decodedData)
        {
            byte[] decodedDataAsBytes = ASCIIEncoding.ASCII.GetBytes(decodedData);
            return Convert.ToBase64String(decodedDataAsBytes);
        }

        #endregion Base64

        #region String

        public static string Digits(this string s)
        {
            return new String(s.Where(Char.IsDigit).ToArray());
        }

        /// <summary>
        /// Get left part of string.
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="length">Left part length</param>
        /// <returns>String</returns>
        public static string Left(this string s, int length)
        {
            return s.Substring(0, length);
        }

        /// <summary>
        /// Get right part of string.
        /// </summary>
        /// <param name="s">String</param>
        /// <param name="length">Right part length</param>
        /// <returns>String</returns>
        public static string Right(this string s, int length)
        {
            return s.Substring(s.Length - length, length);
        }

        /// <summary>
        /// Replace first occurrence of string.
        /// </summary>
        /// <param name="text">Text to be searched</param>
        /// <param name="search">String to search</param>
        /// <param name="replace">String to replace</param>
        /// <returns>String</returns>
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            //Regex regex = new Regex(Regex.Escape("o"));
            //string newText = regex.Replace("A", "B", 1);

            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            else
            {
                return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
            }
        }

        /// <summary>
        /// Split string at Pascal Case upper chars.
        /// </summary>
        /// <param name="s">String</param>
        /// <returns>Splitted string</returns>
        public static string SplitPascalCase(this string s)
        {
            Regex regex = new Regex("(?<=[a-z])(?<x>[A-Z])|(?<=.)(?<x>[A-Z])(?=[a-z])");
            return regex.Replace(s, " ${x}");
        }

        #endregion String

        #region To... | To..Nullable

        /// <summary>
        /// Convert String to Byte[] (Binary).
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Byte[] (Binary) value</returns>
        public static byte[] ToBinary(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

                return encoding.GetBytes(s);
            }
        }

        /// <summary>
        /// Convert String to nullable Byte[] (Binary)
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable Byte[] (Binary) value</returns>
        public static byte[] ToBinaryNullable(this string s)
        {
            return ToBinary(s);
        }

        /// <summary>
        /// Convert String to Boolean.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Boolean value</returns>
        public static bool ToBoolean(this string s)
        {
            return (s.ToUpper() == Boolean.TrueString.ToUpper() ? true : false);
        }

        /// <summary>
        /// Convert string to nullable Boolean.
        /// </summary>
        /// <param name="s">string value</param>
        /// <returns>Nullable Boolean value</returns>
        public static bool? ToBooleanNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                return (s.ToUpper() == Boolean.TrueString.ToUpper() ? true : false);
            }
        }

        /// <summary>
        /// Convert String to Byte.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Byte value</returns>
        public static byte ToByte(this string s)
        {
            byte x;

            Byte.TryParse(s.Trim(), out x);

            return x;
        }

        /// <summary>
        /// Convert String to nullable Byte.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable Byte value</returns>
        public static Byte? ToByteNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                byte x;

                Byte.TryParse(s.Trim(), out x);

                return x;
            }
        }

        /// <summary>
        /// Convert String to char.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>char value</returns>
        public static char ToChar(this string s)
        {
            char x;

            char.TryParse(s.Trim(), out x);

            return x;
        }

        /// <summary>
        /// Convert String to nullable char.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable char value</returns>
        public static char? ToCharNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                char x;

                char.TryParse(s.Trim(), out x);

                return x;
            }
        }

        /// <summary>
        /// Convert String to DateTime.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>DateTime value</returns>
        public static DateTime ToDateTime(this string s)
        {
            DateTime x;

            DateTime.TryParse(s.Trim(), out x);

            return x;
        }

        /// <summary>
        /// Convert String to nullable DateTime.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable DateTime value</returns>
        public static DateTime? ToDateTimeNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                DateTime x;

                DateTime.TryParse(s.Trim(), out x);

                return x;
            }
        }

        /// <summary>
        /// Convert String to Decimal.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Decimal value</returns>
        public static decimal ToDecimal(this string s)
        {
            decimal x;

            Decimal.TryParse(s.Trim(), out x);

            return x;
        }

        /// <summary>
        /// Convert String to nullable Decimal.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable Decimal value</returns>
        public static decimal? ToDecimalNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                Decimal x;

                Decimal.TryParse(s.Trim(), out x);

                return x;
            }
        }

        /// <summary>
        /// Convert String to Double.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Double value</returns>
        public static double ToDouble(this string s)
        {
            Double x;

            Double.TryParse(s.Trim(), out x);

            return x;
        }

        /// <summary>
        /// Convert String to nullable Double.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable Double value</returns>
        public static double? ToDoubleNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                Double x;

                Double.TryParse(s.Trim(), out x);

                return x;
            }
        }

        /// <summary>
        /// Convert String to Guid.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Guid value</returns>
        public static Guid ToGuid(this string s)
        {
            return new Guid(s);
        }

        /// <summary>
        /// Convert String to nullable Guid.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable Guid value</returns>
        public static Guid? ToGuidNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                return new Guid(s);
            }
        }

        /// <summary>
        /// Convert String to Int16.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Int16 value</returns>
        public static short ToInt16(this string s)
        {
            short x;

            short.TryParse(s.Trim(), out x);

            return x;
        }

        /// <summary>
        /// Convert String to nullable Int16.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable Int16 value</returns>
        public static short? ToInt16Nullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                short x;

                short.TryParse(s.Trim(), out x);

                return x;
            }
        }

        /// <summary>
        /// Convert String to Int32.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Int32 value</returns>
        public static int ToInt32(this string s)
        {
            int x;

            int.TryParse(s.Trim(), out x);

            return x;
        }

        /// <summary>
        /// Convert String to nullable Int32.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable Int32 value</returns>
        public static int? ToInt32Nullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                int x;

                int.TryParse(s.Trim(), out x);

                return x;
            }
        }

        /// <summary>
        /// Convert String to Int64.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Int64 value</returns>
        public static long ToInt64(this string s)
        {
            long x;

            long.TryParse(s.Trim(), out x);

            return x;
        }

        /// <summary>
        /// Convert String to nullable Int64.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable Int64 value</returns>
        public static long? ToInt64Nullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                long x;

                long.TryParse(s.Trim(), out x);

                return x;
            }
        }

        /// <summary>
        /// Convert String to Object.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Object value</returns>
        public static object ToObject(this string s)
        {
            return (object)s;
        }

        /// <summary>
        /// Convert String to nullable Object.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable Object value</returns>
        public static object ToObjectNullable(this string s)
        {
            return (object)s;
        }

        /// <summary>
        /// Convert String to Single.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Single value</returns>
        public static float ToSingle(this string s)
        {
            float x;

            Single.TryParse(s.Trim(), out x);

            return x;
        }

        /// <summary>
        /// Convert String to nullable Single.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable Single value</returns>
        public static float? ToSingleNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                float x;

                float.TryParse(s.Trim(), out x);

                return x;
            }
        }

        /// <summary>
        /// Convert String to String.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>String value</returns>
        public static string ToString(this string s)
        {
            return s.Trim();
        }

        /// <summary>
        /// Convert String to nullable String.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable string value</returns>
        public static string ToStringNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                return s.Trim();
            }
        }

        /// <summary>
        /// Convert String to TimeSpan.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>TimeSpan value</returns>
        public static TimeSpan ToTimeSpan(this string s)
        {
            TimeSpan x;

            TimeSpan.TryParse(s.Trim(), out x);

            return x;
        }

        /// <summary>
        /// Convert String to nullable TimeSpan.
        /// </summary>
        /// <param name="s">String value</param>
        /// <returns>Nullable TimeSpan value</returns>
        public static TimeSpan? ToTimeSpanNullable(this string s)
        {
            if (String.IsNullOrEmpty(s))
                return null;
            else
            {
                TimeSpan x;

                TimeSpan.TryParse(s.Trim(), out x);

                return x;
            }
        }

        #endregion To... | To..Nullable
    }
}