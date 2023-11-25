using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ProNotes.AppLib.MVC.Defaults
{
    public class MvcNewtonsoftJsonOptionsDefaults
    {
        public Action<MvcNewtonsoftJsonOptions> GetDefaults()
        {
            Action<MvcNewtonsoftJsonOptions> defaults = options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver(); // Pascal casing
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Culture = new System.Globalization.CultureInfo("tr-TR");
            };

            return defaults;
        }
    }
}