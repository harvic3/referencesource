﻿using System.Diagnostics.Contracts;
using System.Globalization;

namespace System.Net.Http.Headers
{
    internal class TimeSpanHeaderParser : BaseHeaderParser
    {
        internal static readonly TimeSpanHeaderParser Parser = new TimeSpanHeaderParser();

        private TimeSpanHeaderParser()
            : base(false)
        {
        }

        public override string ToString(object value)
        {
            Contract.Assert(value is TimeSpan);

            return ((int)((TimeSpan)value).TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
        }

        protected override int GetParsedValueLength(string value, int startIndex, object storeValue,
            out object parsedValue)
        {
            parsedValue = null;
            
            int numberLength = HttpRuleParser.GetNumberLength(value, startIndex, false);

            if ((numberLength == 0) || (numberLength > HttpRuleParser.MaxInt32Digits))
            {
                return 0;
            }

            int result = 0;
            if (!HeaderUtilities.TryParseInt32(value.Substring(startIndex, numberLength), out result))
            {
                return 0;
            }
            
            parsedValue = new TimeSpan(0, 0, result);
            return numberLength;
        }
    }
}
