using System;

namespace NSwag.Annotations.AzureFunctions
{
    public enum AuthScheme
    {
        Basic,
        Bearer,
        HeaderApiKey,
        QueryApiKey
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OpenApiAuthorizeAttribute : SwaggerAuthorizeAttribute
    {
        public OpenApiAuthorizeAttribute(AuthScheme scheme)
            : base(scheme)
        {
        }
    }

    /// <summary>
    /// Authorize annotation. NOTE! Only an annotation! Does not secure the method in any way!
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    [Obsolete("Use " + nameof(OpenApiAuthorizeAttribute) + " instead.")]
    public class SwaggerAuthorizeAttribute : Attribute
    {
        /// <summary>
        /// Initializes the <see cref="SwaggerAuthorizeAttribute"/> with the given AuthScheme.
        /// </summary>
        /// <param name="scheme"></param>
        public SwaggerAuthorizeAttribute(AuthScheme scheme)
        {
            Scheme = scheme;
        }

        /// <summary>
        /// The authorization scheme.
        /// </summary>
        public AuthScheme Scheme { get; set; }

    }

}
