namespace ProNotes.AppLib.Concrete
{
    public class DataOptions
    {
        public ApplicationSection Application { get; set; }
        public DatabaseSection Database { get; set; }

    }

    public class ApplicationSection
    {
        public string? Name { get; set; }
        public string? Version { get; set; }
        public ApplicationSettingsSection Settings { get; set; }
    }

    public class ApplicationSettingsSection
    {
        public string? WebRootPath { get; set; }
        public string? SharedPath { get; set; }
    }

    public class DatabaseSection
    {
        public string? Provider { get; set; }
        public string? ConnectionString { get; set; }
    }
}