using System;
using System.Linq;
using System.Reflection;

namespace ArhivskiVjesnikLibrary.Common.Helpers
{
    public static class ModelHelper
    {
        private const string ModelsNamespace = "ArhivskiVjesnikLibrary.DAL.Models";
        private const char PlusSeparator = '+';
        private const char DotSeparator = '.';

        public static bool DoesPropertyExistsInModels(string propertyName)
        {
            Type[] types = GetTypesInNamespace(ModelsNamespace);

            string className = propertyName.Split(DotSeparator)[0];
            Type type = types.Where(x => x.Name == className).FirstOrDefault();
            bool isValidClass = types.Any(x => x.Name == className);

            string[] properties = GetProperties(propertyName, className);
            bool areValidProperties = type.GetProperties().Where(x => properties.Contains(x.Name)).Count() > 0;

            return isValidClass && areValidProperties;
        }

        public static bool IsPropertyTextType(string propertyName)
        {
            return GetPropertyTypeByPropertyName(propertyName) == typeof(string);
        }

        public static bool IsPropertyNumericType(string propertyName)
        {
            return GetPropertyTypeByPropertyName(propertyName) == typeof(int) || GetPropertyTypeByPropertyName(propertyName) == typeof(double);
        }

        public static bool IsPropertyDateTimeType(string propertyName)
        {
            return GetPropertyTypeByPropertyName(propertyName) == typeof(DateTime);
        }

        private static Type GetPropertyTypeByPropertyName(string propertyName)
        {
            Type[] types = GetTypesInNamespace(ModelsNamespace);

            string className = propertyName.Split(DotSeparator)[0];
            Type type = types.Where(x => x.Name == className).FirstOrDefault();

            string[] properties = GetProperties(propertyName, className);

            return type.GetProperty(properties[0]).PropertyType;
        }

        private static string[] GetProperties(string propertyName, string className)
        {
            return propertyName.Split(PlusSeparator).Select(x => x.Replace($"{className}.", string.Empty)).ToArray();
        }

        private static Type[] GetTypesInNamespace(string nameSpace)
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsClass)
                .Where(x => x.Namespace == nameSpace)
                .ToArray();
        }
    }
}
