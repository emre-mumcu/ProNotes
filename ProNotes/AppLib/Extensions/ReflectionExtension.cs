using System.Reflection;
using System.Text;

namespace ProNotes.AppLib.Extensions
{
    public static class ReflectionExtension
    {
        /// <returns>Returns the public instance properties of a class as comma seperated string</returns>
        public static string GetPublicPropertyNames<T>(this T obj) where T : class, new()
        {
            if (obj == null) throw new ArgumentException(nameof(obj));

            List<PropertyInfo> props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();

            StringBuilder sb = new StringBuilder();

            sb.AppendJoin(',', props.Select(p => p.Name).ToList());

            return sb.ToString();
        }
    }
}