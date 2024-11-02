using System.Text;

namespace RtuTools.Extensions
{
    public static class StringExtension
    {
        public static string ToHexString(this string value, bool replace = true)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var hexString = BitConverter.ToString(bytes);

            return replace ? hexString.Replace("-", "") : hexString;
        }

        public static byte[] ToBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
        
        public static byte[] ToBytesFromHexString(this string hexString)
        {
            return Enumerable.Range(0, hexString.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
