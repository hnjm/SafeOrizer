using System.IO;
using System.Threading.Tasks;

namespace SafeOrizer.Helpers
{
    public static class Converters
    {
        public static async Task<byte[]> ReadFullyAsync(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                await input.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
