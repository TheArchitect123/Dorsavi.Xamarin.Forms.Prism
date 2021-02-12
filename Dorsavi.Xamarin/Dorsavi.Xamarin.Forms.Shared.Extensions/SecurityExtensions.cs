using Plugin.Security.Core;

namespace Dorsavi.Xamarin.Forms.Shared.Extensions
{
    public static class SecurityExtensions
    {
        public static string HashPassword(this string clearText) => new PasswordEncoder().Encode(clearText, EncryptType.SHA_512);
    }
}
