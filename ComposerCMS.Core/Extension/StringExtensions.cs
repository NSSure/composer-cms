using System.Linq;

namespace ComposerCMS.Core
{
    public static class StringExtensions
    {
        public static string NormalizeForUrl(this string value)
        {
            return string.Join('-', value.Split(Constants.Environment.EmptySpace).Select(a => a.ToLower()));
        }

        /// <summary>
        /// Trims and forward slashes and the beginning and end of the value and then adds
        /// a single forward slash back to the beginning of the string.
        /// </summary>
        /// <param name="url">The url to verify the integrity of the slash leading and trailing slash structure.</param>
        /// <returns></returns>
        public static string VerifyForwardSlash(this string url)
        {
            return $"/{url.Trim('/')}";
        }
    }
}
