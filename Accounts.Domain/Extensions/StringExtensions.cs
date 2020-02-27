using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Accounts.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string Sha256(
            this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string
                    .Empty;

            using var sha = SHA256.Create();

            var bytes = Encoding
                .UTF8
                .GetBytes(input);

            var hash = sha
                .ComputeHash(bytes);

            return Convert
                .ToBase64String(hash);
        }

        public static bool IsEmail(
            this string input)
        {
            const string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

            var regex = new Regex(
                pattern,
                options);

            return regex.IsMatch(input);
        }
    }
}
