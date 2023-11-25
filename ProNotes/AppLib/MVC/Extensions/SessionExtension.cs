using Newtonsoft.Json;
using System.Text;

namespace ProNotes.AppLib.MVC.Extensions
{
    public static class SessionExtension
    {
        private static JsonSerializerSettings jsonSerializerSettings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    Formatting = Formatting.None,
                    PreserveReferencesHandling = PreserveReferencesHandling.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    Culture = new System.Globalization.CultureInfo("tr-TR")
                };
            }
        }

        public static void SetKey<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, Formatting.None, jsonSerializerSettings));
        }

        public static T? GetKey<T>(this ISession session, string key)
        {
            string? value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static void RemoveKey(this ISession session, string key)
        {
            session.Remove(key);
        }

        public static string GetSessionId(this ISession session)
        {
            return session.Id;
        }

        public static void CreateUserSession(this ISession session, string userIdentifier)
        {
            if (string.IsNullOrEmpty(userIdentifier)) throw new ArgumentException("Value can no tbe null.", nameof(userIdentifier));

            session.SetKey("login", true);
            session.SetKey("userid", userIdentifier);
        }

        public static bool ValidateUserSession(this ISession session)
        {
            var login = session.GetKey<bool>("login");
            var userIdentifier = session.GetKey<string>("userid");
            return login && !string.IsNullOrEmpty(userIdentifier);
        }
    }
}