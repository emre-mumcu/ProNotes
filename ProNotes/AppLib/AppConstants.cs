namespace ProNotes.AppLib
{
    public static class AppConstants
    {
        // SESSION VARIABLES       
        public const string Session_Cookie_Name = "app.cookie.session";
        public const int Session_IdleTimeout = 120;

        // SESSION KEYS
        public const string SessionKey_Login = "LOGIN";
        public const string SessionKey_Captcha = "CAPTCHA";
        public const string SessionKey_LoginUser = "LOGINUSER";
        public const string SessionKey_SelectedRole = "SELECTED_ROLE";
        public const string SessionKey_System_Message = "SYSTEM_MESSAGE";

        // AUTHENTICATION VARIABLES
        public const string Auth_Cookie_Name = "app.cookie.authentication";
        public const string Auth_Cookie_LoginPath = "/Login";
        public const string Auth_Cookie_LogoutPath = "/Logout";
        public const string Auth_Cookie_AccessDeniedPath = "/AccessDenied";
        public const string Auth_Cookie_ClaimsIssuer = "app.cookie.issuer";
        public const string Auth_Cookie_ReturnUrlParameter = "AppReturnUrl";
        public const string Auth_Cookie_RedirectUrl = "AppRedirectUrl";
        public const int Auth_Cookie_ExpireTimeSpan = 120;
    }
}
