using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pamux.Assets.Scripts.Pamux.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue)
        {
            if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
            { 
                return defaultValue;
            }

            return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
        }
    }
}
