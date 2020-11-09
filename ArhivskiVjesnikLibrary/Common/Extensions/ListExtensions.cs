using System.Collections.Generic;
using System.Linq;

namespace ArhivskiVjesnikLibrary.Common.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<KeyValuePair<TEnum, string>> ToEnumKeyValuePairs<TEnum>(this IEnumerable<TEnum> enumList) where TEnum : struct
        {
            return enumList.Select(x => new KeyValuePair<TEnum, string>(x, x.GetDescription()));
        }

        public static IEnumerable<KeyValuePair<TEnum?, string>> ToEnumKeyValuePairs<TEnum>(this IEnumerable<TEnum?> enumList) where TEnum : struct
        {
            return enumList.Select(x => new KeyValuePair<TEnum?, string>(x.Value, x.Value.GetDescription()));
        }
    }
}
