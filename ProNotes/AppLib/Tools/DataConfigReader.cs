namespace ProNotes.AppLib.Tools
{
    public static class DataConfigReader
    {
        public readonly static IConfigurationRoot _dataConfig;

        static DataConfigReader()
        {
            _dataConfig = new ConfigurationBuilder().AddJsonFile("data.json").Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section">Target Section. Use : for multi-level sections</param>
        /// <param name="parameter">Name of the parameter under the target section to get its value</param>
        /// <param name="isProtected">If value is DPAPI protected or not</param>
        /// <returns></returns>
        public static string GetParameter(string? section, string parameter, bool isProtected = false)
        {
            IConfigurationSection targetSection = _dataConfig.GetSection(section);

            string readValue = String.IsNullOrEmpty(section) ? _dataConfig.GetSection(parameter).Value : targetSection.GetSection(parameter).Value;

            if (isProtected) return DPAPI.Unportect(readValue, parameter);
            else return readValue;
        }

        public static T GetParameter<T>(string section, string parameter, bool isProtected = false)
        {
            return (T)Convert.ChangeType(GetParameter(section, parameter, isProtected), typeof(T));
        }
    }
}
