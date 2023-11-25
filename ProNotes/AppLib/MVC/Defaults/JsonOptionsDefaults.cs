using Microsoft.AspNetCore.Mvc;

namespace ProNotes.AppLib.MVC.Defaults
{
    public class JsonOptionsDefaults
    {
        public Action<JsonOptions> GetDefaults()
        {
            Action<JsonOptions> defaults = options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
            };

            return defaults;
        }
    }
}