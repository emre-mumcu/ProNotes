using Microsoft.AspNetCore.Authorization;

namespace ProNotes.AppLib.MVC.Defaults
{
    public class AuthorizationOptionsDefaults
    {
        public Action<AuthorizationOptions> GetDefaults(Dictionary<string, AuthorizationPolicy>? AuthorizationPolicies = null)
        {
            Action<AuthorizationOptions> defaults = options =>
            {
                /*
                 * Gets or sets the default authorization policy with no policy name specified.
                 * Defaults to require authenticated users.
                 *
                 * The DefaultPolicy is the policy that is applied when:
                 *      (*) You specify that authorization is required, either using RequireAuthorization(), by applying an AuthorizeFilter,
                 *          or by using the[Authorize] attribute on your actions / Razor Pages.
                 *      (*) You don't specify which policy to use.
                 *
                 */
                options.DefaultPolicy = AuthorizationPolicyLibrary.defaultPolicy;

                /*
                 * Gets or sets the fallback authorization policy when no IAuthorizeData have been provided.
                 * By default the fallback policy is null.
                 *
                 * The FallbackPolicy is applied when the following is true:
                 *      (*) The endpoint does not have any authorisation applied. No[Authorize] attribute, no RequireAuthorization, nothing.
                 *      (*) The endpoint does not have an[AllowAnonymous] applied, either explicitly or using conventions.
                 *
                 * So the FallbackPolicy only applies if you don't apply any other sort of authorization policy,
                 * including the DefaultPolicy, When that's true, the FallbackPolicy is used.
                 * By default, the FallbackPolicy is a no - op; it allows all requests without authorization.
                 *
                 */
                options.FallbackPolicy = AuthorizationPolicyLibrary.fallbackPolicy;


                // Additional Policies
                if (AuthorizationPolicies != null)
                {
                    foreach (var authorizationPolicyEntry in AuthorizationPolicies)
                    {
                        options.AddPolicy(nameof(authorizationPolicyEntry.Key), authorizationPolicyEntry.Value);
                    }
                }
            };

            return defaults;
        }
    }
}