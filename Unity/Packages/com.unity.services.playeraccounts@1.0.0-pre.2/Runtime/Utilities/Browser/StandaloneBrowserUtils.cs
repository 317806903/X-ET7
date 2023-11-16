#if UNITY_EDITOR || UNITY_STANDALONE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UnityEngine;

namespace Unity.Services.PlayerAccounts
{
    class StandaloneBrowserUtils : IBrowserUtils
    {
        public static HttpListener httpListener;
        public event Action<string> AuthCodeReceivedEvent;

        public async Task LaunchUrl(string url)
        {
            Application.OpenURL(url);
            httpListener.Start();
            var context = await httpListener.GetContextAsync();
            SendBrowserResponse(context.Response, httpListener);
            var uri = new Uri(url);
            var code = GetAuthCode(context, Unity.Services.PlayerAccounts.HttpUtilities.ParseQueryString(uri.Query)["state"]);
            AuthCodeReceivedEvent?.Invoke(code);
        }

        public void Dismiss()
        {
        }

        static void SendBrowserResponse(HttpListenerResponse response, HttpListener http)
        {
            var responseString = "<html><body><b>DONE!</b><br>(You can return to your app and close this tab/window now)";
            var buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            var responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith(_ =>
            {
                responseOutput.Close();
                http.Stop();
            });
        }

        static string GetAuthCode(HttpListenerContext context, string state)
        {
            var code = context.Request.QueryString.Get("code");
            var error = context.Request.QueryString.Get("error");
            var incomingState = context.Request.QueryString.Get("state");
            var uri = new Uri(context.Request.Url.AbsoluteUri);

            if (string.IsNullOrEmpty(code))
            {
                var fragment = Unity.Services.PlayerAccounts.HttpUtilities.ParseQueryString(uri.Fragment);
                code = fragment["code"];
                error ??= fragment["error"];
                incomingState = fragment["state"];
            }

            if (!string.IsNullOrEmpty(error))
            {
                throw PlayerAccountsExceptionHandler.HandleError(error);
            }

            if (incomingState != state)
            {
                throw PlayerAccountsException.Create(PlayerAccountsErrorCodes.InvalidState, $"Received request with invalid state ({incomingState})");
            }

            return code;
        }
    }
}
#endif
