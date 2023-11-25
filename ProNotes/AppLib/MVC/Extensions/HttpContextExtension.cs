using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Primitives;
using ProNotes.AppLib.Extensions;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ProNotes.AppLib.MVC.Extensions
{
    public static class HttpContextExtension
    {
        public static (IPAddress? Ip, string IpList) GetClientIP(this HttpContext context)
        {
            List<IPAddress> list = new List<IPAddress>();

            list.AddIfNotExists(context?.Features?.Get<IHttpConnectionFeature>()?.LocalIpAddress);
            list.AddIfNotExists(context?.Features?.Get<IHttpConnectionFeature>()?.RemoteIpAddress);
            list.AddIfNotExists(context?.Request?.HttpContext?.Connection?.LocalIpAddress);
            list.AddIfNotExists(context?.Request?.HttpContext?.Connection?.RemoteIpAddress);
            list.AddIfNotExists(IPAddress.TryParse(context?.GetHeaderValue("CF-Connecting-IP"), out IPAddress? ip1) ? ip1 : null);
            list.AddIfNotExists(IPAddress.TryParse(context?.GetHeaderValue("X-Forwarded-For"), out IPAddress? ip2) ? ip2 : null);
            list.AddIfNotExists(IPAddress.TryParse(context?.GetHeaderValue("X-Original-Forwarded-For"), out IPAddress? ip3) ? ip3 : null);
            list.AddIfNotExists(IPAddress.TryParse(context?.GetHeaderValue("X-Real-IP"), out IPAddress? ip4) ? ip4 : null);
            list.AddIfNotExists(IPAddress.TryParse(context?.GetHeaderValue("REMOTE-ADDR"), out IPAddress? ip5) ? ip5 : null);

            StringBuilder ipAddressList = new StringBuilder();
            ipAddressList.AppendJoin(',', list);

            return (list.FirstOrDefault(), ipAddressList.ToString());
        }

        public static (IPAddress? Ip, string IpList) GetServerIP()
        {
            //System.Net.NetworkInformation.NetworkInterface[] nics = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            //foreach (System.Net.NetworkInformation.NetworkInterface adapter in nics)

            List<IPAddress> list = Dns.GetHostEntry(Dns.GetHostName()).AddressList.ToList();

            StringBuilder ipAddressList = new StringBuilder();
            ipAddressList.AppendJoin(',', list);


            return (list.FirstOrDefault(i => i.AddressFamily == AddressFamily.InterNetwork), ipAddressList.ToString());
        }

        public static string? GetProtocol(this HttpContext context)
        {
            return context.GetHeaderValue("x-forwarded-proto");
        }

        public static string? GetHost(this HttpContext context)
        {
            return context.GetHeaderValue("x-forwarded-host");
        }

        public static string? GetUserAgent(this HttpContext context)
        {
            return context.GetHeaderValue("user-agent");
        }

        public static string? GetHeaderValue(this HttpContext context, string HeaderKey)
        {
            if (context.Request.Headers.TryGetValue(HeaderKey, out StringValues HeaderValue))
                return HeaderValue;
            else
                return null;
        }
    }
}

/* 
HttpContext.Connection.RemoteIpAddress.ToString() would only get the IP address of the reverse proxy or load balancer
if there is a reverse proxy or load balancer before the server. To solve this issue, and get the client real IP address, 
use the HTTP header "X-Forwarded-For".

The X-Forwarded-For (XFF) header is a de-facto standard header for identifying the originating IP address of a client 
connecting to a web server through an HTTP proxy or a load balancer. The header is a list of comma separated IP addresses.

However, blindly trust the "X-Forwarded-For" header would also be problematic. Hackers can easily manipulate this header 
to provide false client IP address. There is no perfect solution for this. 

ForwardedHeadersMiddleware
The Forwarded Headers Middleware in asp.net core X-Forwarded-For, X-Forwarded-Proto and X-Forwarded-Host headers 
and fills in the associated fields on HttpContext.

app.UseForwardedHeaders(new ForwardedHeadersOptions {
ForwardedHeaders = ForwardedHeaders.All, // ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
RequireHeaderSymmetry = false,
ForwardLimit = null,
KnownNetworks = { new IPNetwork(IPAddress.Parse("::ffff:172.17.0.1"), 104) }
});

using Microsoft.AspNetCore.HttpOverrides;
Request.HttpContext.Connection.RemoteIpAddress 
*/