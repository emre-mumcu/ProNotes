``` csharp

    services.Configure<RequestLocalizationOptions>(options =>
    {
        options.DefaultRequestCulture = new RequestCulture(new CultureInfo("tr-TR"));
        options.SupportedCultures = new[] { new CultureInfo("tr-TR") };
        options.SupportedUICultures = new[] { new CultureInfo("tr-TR") };
        options.RequestCultureProviders = new List<IRequestCultureProvider> {
            new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider()
        };
    });

        services.AddSession(options =>
        {
            options.Cookie.Name = $"__Session-{Assembly.GetExecutingAssembly().ManifestModule.ModuleVersionId}";
            options.IdleTimeout = TimeSpan.FromMinutes(20);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

    services
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options =>
        {
            options.Cookie.Name = AppConstants.Auth_Cookie_Name;
            options.LoginPath = AppConstants.Auth_Cookie_LoginPath;
            options.LogoutPath = AppConstants.Auth_Cookie_LogoutPath;
            options.AccessDeniedPath = AppConstants.Auth_Cookie_AccessDeniedPath;
            options.ClaimsIssuer = AppConstants.Auth_Cookie_ClaimsIssuer;
            options.ReturnUrlParameter = AppConstants.Auth_Cookie_ReturnUrlParameter;
            options.SlidingExpiration = true;
            options.Cookie.HttpOnly = true; // false: xss vulnerability !!!
            options.ExpireTimeSpan = TimeSpan.FromMinutes(AppConstants.Auth_Cookie_ExpireTimeSpan);
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Validate();
            options.EventsType = typeof(CustomCookieAuthenticationEvents);
            options.Events = new CookieAuthenticationEvents()
            {
                OnValidatePrincipal = context =>
                {
                    return Task.CompletedTask;
                }
            };
        })
    ;

```