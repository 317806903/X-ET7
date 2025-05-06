using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ET
{
	public static class HttpHelper
	{
        public static async Task<(bool, byte[])> DownloadFileBytesAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Log.Error("Empty URL, return an empty task.");
                return (false, null);
            }

            try
            {
                using(HttpClient httpClient = new())
                {
                    byte[] data = await httpClient.GetByteArrayAsync(url);
                    return (true, data);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return (false, null);
            }
        }

        public static async Task<(bool, string)> DownloadFileTextAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Log.Info("Empty URL, return an empty task.");
                return (false, null);
            }

            try
            {
                using(HttpClient httpClient = new())
                {
                    string data = await httpClient.GetStringAsync(url);
                    return (true, data);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                return (false, null);
            }
        }
	}
}
