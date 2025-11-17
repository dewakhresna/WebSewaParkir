using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace KandangMobil.Helpers
{
    public class HashHelper
    {
        //public static string Hash(string input)
        //{
        //    using (var sha = SHA256.Create())
        //    {
        //        var bytes = Encoding.UTF8.GetBytes(input);
        //        var hash = sha.ComputeHash(bytes);
        //        return Convert.ToHexString(hash);
        //    }
        //}

        public static string ToSha256(string input)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
