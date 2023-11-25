using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using ProNotes.AppLib.Abstract;
using ProNotes.AppLib.MVC.Extensions;
using System.Security.Claims;

namespace ProNotes.AppLib.MVC.Evaluators
{
    public class MockPolicyEvaluator : IPolicyEvaluator
    {
        private readonly IAuthenticate _authenticator;
        private readonly IAuthorize _authorizer;


        public MockPolicyEvaluator(IAuthorize authorizer, IAuthenticate authenticator)
        {
            _authenticator = authenticator;
            _authorizer = authorizer;
        }

        /// <summary>
        /// Does authentication for <see cref="AuthorizationPolicy.AuthenticationSchemes"/> and sets the resulting
        /// <see cref="ClaimsPrincipal"/> to <see cref="HttpContext.User"/>.  If no schemes are set, this is a no-op.
        /// </summary>
        /// <param name="policy">The <see cref="AuthorizationPolicy"/>.</param>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <returns><see cref="AuthenticateResult.Success"/> unless all schemes specified by <see cref="AuthorizationPolicy.AuthenticationSchemes"/> failed to authenticate.
        public virtual async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            //if (policy.AuthenticationSchemes != null && policy.AuthenticationSchemes.Count > 0)
            //{
            //    ClaimsPrincipal? newPrincipal = null;

            //    foreach (var scheme in policy.AuthenticationSchemes)
            //    {
            //        var result = await context.AuthenticateAsync(scheme);

            //        if (result != null && result.Succeeded)
            //        {
            //            newPrincipal = MergeUserPrincipal(newPrincipal, result.Principal);
            //        }
            //    }

            //    if (newPrincipal != null)
            //    {
            //        context.User = newPrincipal;
            //        return AuthenticateResult.Success(new AuthenticationTicket(newPrincipal, string.Join(";", policy.AuthenticationSchemes)));
            //    }
            //    else
            //    {
            //        context.User = new ClaimsPrincipal(new ClaimsIdentity());
            //        return AuthenticateResult.NoResult();
            //    }
            //}

            try
            {
                // If user requested the Account controller, do NOT engage
                if (context.Request.Path.Value != null && (context.Request.Path.Value.StartsWith("/Logout") || context.Request.Path.Value.StartsWith("/Login")))
                    return await Task.FromResult(AuthenticateResult.NoResult());

                string username = "MockUser";

                AuthenticationTicket? ticket = await _authorizer.AuthorizeUserAsync(username);

                if (ticket is not null)
                {
                    context.User = ticket.Principal; // Set User

                    context.Session.SetKey(AppConstants.SessionKey_Login, true);
                    context.Session.SetKey(AppConstants.SessionKey_LoginUser, username);

                    // Return success
                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("ticket is null");
                }


            }
            catch (Exception ex)
            {
                //context.Response.Redirect("/Account/Login");
                return await Task.FromResult(AuthenticateResult.Fail(ex.Message));
            }
        }

        /// <summary>
        /// Attempts authorization for a policy using <see cref="IAuthorizationService"/>.
        /// </summary>
        /// <param name="policy">The <see cref="AuthorizationPolicy"/>.</param>
        /// <param name="authenticationResult">The result of a call to <see cref="AuthenticateAsync(AuthorizationPolicy, HttpContext)"/>.</param>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="resource">
        /// An optional resource the policy should be checked with.
        /// If a resource is not required for policy evaluation you may pass null as the value.
        /// </param>
        /// <returns>Returns <see cref="PolicyAuthorizationResult.Success"/> if authorization succeeds.
        /// Otherwise returns <see cref="PolicyAuthorizationResult.Forbid"/> if <see cref="AuthenticateResult.Succeeded"/>, otherwise
        /// returns  <see cref="PolicyAuthorizationResult.Challenge"/></returns>
        public virtual async Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
        {
            if (policy == null)
                throw new ArgumentNullException(nameof(policy));

            // TODO: What to do ?
            //var result = await _authorization.AuthorizeAsync(context.User, resource, policy);
            //if (result.Succeeded) return PolicyAuthorizationResult.Success();

            await Task.FromResult(0);

            // If authentication was successful, return forbidden, otherwise challenge
            return authenticationResult.Succeeded ? PolicyAuthorizationResult.Forbid() : PolicyAuthorizationResult.Challenge();
        }

        /// <summary>
        /// Add all ClaimsIdentities from an additional ClaimPrincipal to the ClaimsPrincipal
        /// Merges a new claims principal, placing all new identities first, and eliminating
        /// any empty unauthenticated identities from context.User
        /// </summary>
        /// <param name="existingPrincipal">The <see cref="ClaimsPrincipal"/> containing existing <see cref="ClaimsIdentity"/>.</param>
        /// <param name="additionalPrincipal">The <see cref="ClaimsPrincipal"/> containing <see cref="ClaimsIdentity"/> to be added.</param>
        public static ClaimsPrincipal MergeUserPrincipal(ClaimsPrincipal? existingPrincipal, ClaimsPrincipal? additionalPrincipal)
        {
            // For the first principal, just use the new principal rather than copying it
            if (existingPrincipal == null && additionalPrincipal != null)
            {
                return additionalPrincipal;
            }

            var newPrincipal = new ClaimsPrincipal();

            // New principal identities go first
            if (additionalPrincipal != null)
            {
                newPrincipal.AddIdentities(additionalPrincipal.Identities);
            }

            // Then add any existing non empty or authenticated identities
            if (existingPrincipal != null)
            {
                newPrincipal.AddIdentities(existingPrincipal.Identities.Where(i => i.IsAuthenticated || i.Claims.Any()));
            }
            return newPrincipal;
        }

    }
}
