using Newtonsoft.Json;
using System.Collections;

namespace ProNotes.AppLib.Serialization
{
    public static class ExceptionSerialization
    {
        public static string ToJson(this Exception ex)
        {
            var error = new Dictionary<string, string>
        {
            {"UtcTime", DateTime.UtcNow.ToString("yyyy.MM.dd HH.mm.ss.FFFFFFF") },
            {"Timezone", TimeZoneInfo.Local.DisplayName },
            {"Type", ex.GetType().ToString()},
            {"Message", ex.Message},
            {"StackTrace", ex.StackTrace ?? string.Empty}
        };

            foreach (DictionaryEntry data in ex.Data)
            {
                if (data.Key != null && data.Value != null)
                {
                    error.Add(data.Key.ToString()!, data.Value.ToString()!);
                }
            }

            return JsonConvert.SerializeObject(error, Formatting.Indented);
        }
    }
}