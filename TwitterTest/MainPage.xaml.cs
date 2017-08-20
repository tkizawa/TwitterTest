using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CoreTweet;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace TwitterTest
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const string API_KEY = "ssdQq2UnxTxyC3fKWkEd4a1Wf";
        public const string API_SECRET = "Iszzvbdejo6pqHLaN3xu7ag2cnU9J194galwFQE6GciGnwwJGM";
        public const string AP_CALLBACK_URL = "";
        public const string TOKEN = "17708800-nlBQ8ZGZnG7llQrmJdpHW9BxlEdyi5C9MmQu7a8hF";
        public const string TOKEN_SECRET = "2szjJXLglKcIBBSITgj7l9YIY4cKmCW2rHVubUhApr2Ry";

        //OAuth2Token token;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "Hello World!!";
            //Tokens tokens = await AuthenticationAsync();
            Tokens tokens = TwitterAuth();

            var home = await tokens.Statuses.HomeTimelineAsync();

            txtResult.Text = home.Json.ToString();
         }

        /// <summary>
        /// コンシューマキー、コンシューマシークレット、アクセストークン、アクセスシークレットがある場合の認証
        /// </summary>
        /// <returns></returns>
        public Tokens TwitterAuth()
        {
            var tokens = Tokens.Create(API_KEY, API_SECRET, TOKEN, TOKEN_SECRET);
            return tokens;
        }

        public async Task<Tokens> AuthenticationAsync()
        {
            var redirectUrl = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            var session = await OAuth.AuthorizeAsync(API_KEY, API_SECRET, redirectUrl.ToString());

            var broker = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None,
                session.AuthorizeUri, redirectUrl);

            if (broker.ResponseStatus == WebAuthenticationStatus.Success)
            {
                var responseUri = new Uri(broker.ResponseData);
                var decoder = new WwwFormUrlDecoder(responseUri.Query);
                var tokens = await session.GetTokensAsync(decoder.GetFirstValueByName("oauth_verifier"));

                return tokens;
            }
            else
            {
                // エラー発生
            }

            return null;
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "Hello World!!";
            //Tokens tokens = await AuthenticationAsync();
            Tokens tokens = TwitterAuth();

            var text = TextSend.Text;
            tokens.Statuses.UpdateAsync(new Dictionary<string, object>() { { "status", text } });
        }
    }
}
