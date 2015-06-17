namespace Trickbox
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    class UseConfigureAwait
    {
        public static async Task<String> GetResponseAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                // If in doubt (no GUI application; no sync. context) supply .ConfigureAwait(false)
                var response = await client.GetStringAsync(uri).ConfigureAwait(false);
                return response;
            }
        }
    }
}
