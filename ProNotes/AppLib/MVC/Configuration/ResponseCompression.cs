using Microsoft.AspNetCore.ResponseCompression;
using System;
using System.IO.Compression;

namespace ProNotes.AppLib.MVC.Configuration
{
    public static class ResponseCompression
    {
        /// <summary>
        /// If no compression providers are explicitly added to the CompressionProviderCollection the Brotli compression provider and Gzip compression provider are added by default to the array of compression providers. 
        /// Compression defaults to Brotli compression when the Brotli compressed data format is supported by the client. 
        /// If Brotli isn't supported by the client, compression defaults to Gzip when the client supports Gzip compression.
        /// </summary>
        public static IServiceCollection _AddResponseCompression(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                // Setting EnableForHttps to true is a security risk.
                // options.EnableForHttps = true;

                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();

                // Replace or append MIME types with ResponseCompressionOptions.MimeTypes. Note that wildcard MIME types, such as text/* aren't supported. 
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });

            return services;
        }

        /// <summary>
        /// app.UseResponseCompression must be called before any middleware that compresses responses. 
        /// </summary>
        public static IApplicationBuilder _UseResponseCompression(this WebApplication app)
        {
            app.UseResponseCompression();

            return app;
        }
    }
}
