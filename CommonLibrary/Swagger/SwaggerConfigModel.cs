using System;

namespace CommonLibrary.Swagger
{
    public sealed class SwaggerConfigModel
    {
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Version { get; set; }
        public string? Url { get; set; }
        public string? RoutePrefix { get; set; }

        public bool IsConfigured()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Title) &&
                   !string.IsNullOrWhiteSpace(Version) &&
                   Uri.TryCreate(
                       Url,
                       UriKind.Absolute,
                       out _);
        }
    }
}
