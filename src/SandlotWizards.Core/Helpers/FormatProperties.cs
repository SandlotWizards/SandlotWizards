using System.Reflection;

namespace SandlotWizards.Core.Helpers
{
    public static class PropertyFormatter
    {
        /// <summary>
        /// Formats all public readable properties of an object into a comma-separated string of key=value pairs.
        /// </summary>
        /// <param name="obj">The object to inspect.</param>
        /// <returns>A string like "Prop1=Val1, Prop2=Val2".</returns>
        public static string Format(object obj)
        {
            if (obj == null) return string.Empty;

            var properties = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead)
                .Select(p =>
                {
                    var value = p.GetValue(obj);
                    return $"{p.Name}={value}";
                });

            return string.Join(", ", properties);
        }
    }
}
