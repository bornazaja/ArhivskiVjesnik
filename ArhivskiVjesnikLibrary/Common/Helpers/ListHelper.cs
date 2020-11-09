using ArhivskiVjesnikLibrary.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArhivskiVjesnikLibrary.Common.Helpers
{
    public static class ListHelper
    {
        public static IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePairs<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            return keys.Zip(values, (k, v) => new KeyValuePair<TKey, TValue>(k, v));
        }

        public static IEnumerable<KeyValuePair<TEnum, string>> ToEnumValuesAndDescriptions<TEnum>() where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new Exception("Type must be enum.");
            }

            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(x => new KeyValuePair<TEnum, string>(x, x.GetDescription()));
        }

        public static IEnumerable<KeyValuePair<TEnum?, string>> ToNullableEnumValuesAndDescriptions<TEnum>() where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new Exception("Type must be enum.");
            }

            return Enum.GetValues(typeof(TEnum)).Cast<TEnum?>().Select(x => new KeyValuePair<TEnum?, string>(x.Value, x.Value.GetDescription()));
        }
    }
}
