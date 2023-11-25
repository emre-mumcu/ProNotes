using System.Runtime.CompilerServices;

namespace ProNotes.AppLib.MVC.Attributes
{
    // [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class MenuItemAttribute : Attribute
    {
        public int MenuIndex { get; private set; }
        public string? MenuDisplayName { get; private set; }
        public string MenuIconClass { get; private set; }
        public int SubmenuIndex { get; private set; }
        public string? SubmenuDisplayName { get; private set; }
        public string SubmenuIconClass { get; private set; }
        public string? Link { get; private set; }

        public MenuItemAttribute(
            int _MenuIndex = 0,
            string? _MenuDisplayName = null,
            string _MenuIconClass = "fa-solid fa-angles-right",
            int _SubmenuIndex = 0,
            string? _SubmenuDisplayName = null,
            string _SubmenuIconClass = "fa-solid fa-circle fa-2xs",
            string? _Link = null,
            [CallerMemberName] string? MethodOrPropertyName = null,
            [CallerFilePath] string? SourceFilePath = null
            )
        {
            MenuIndex = _MenuIndex;
            MenuDisplayName = _MenuDisplayName ?? Path.GetFileNameWithoutExtension(SourceFilePath)?.Replace("Controller", null);
            MenuIconClass = _MenuIconClass;
            SubmenuIndex = _SubmenuIndex;
            SubmenuDisplayName = _SubmenuDisplayName ?? MethodOrPropertyName;
            SubmenuIconClass = _SubmenuIconClass;
            Link = _Link;
        }
    }
}
