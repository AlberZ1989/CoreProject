using Microsoft.AspNetCore.Http;

namespace CoreWebApi.Middleware
{
     /// <summary>
    /// This is used by the CookieAuthenticationMiddleware to process request and response cookies.
    /// It is abstracted from the normal cookie APIs to allow for complex operations like chunking.
    /// </summary>
    public interface ICusCookieManager
    {
        /// <summary>
        /// Retrieve a cookie of the given name from the request.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetRequestCookie(HttpContext context, string key);

        /// <summary>
        /// Append the given cookie to the response.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        void AppendResponseCookie(HttpContext context, string key, string value, CookieOptions options);

        /// <summary>
        /// Append a delete cookie to the response.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="options"></param>
        void DeleteCookie(HttpContext context, string key, CookieOptions options);
    }
}