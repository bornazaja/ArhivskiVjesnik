using System;
using System.ComponentModel;
using System.Reflection;

namespace ArhivskiVjesnikLibrary.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum enumerationValue) where TEnum : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("Value must be type of Enum.");
            }

            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            return enumerationValue.ToString();
        }
    }
}
