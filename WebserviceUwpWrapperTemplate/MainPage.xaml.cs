using System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace WebserviceUwpWrapperTemplate
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //TODO:1．使用したいウェブアプリケーションのURLを指定。http://,https://から指定すること。
            WebView_Main.Navigate(new Uri("https://www.google.com"));

            //システムの戻るボタンが押されたらWebviewのGoBackを実行する
            SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
            {
                if(WebView_Main.CanGoBack)
                {
                    WebView_Main.GoBack();
                    args.Handled = true;
                }
            };
        }

        private async void WebView_Main_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            //オフラインではなく、指定したページが正常に読み込まれた場合の処理
            try
            {
                //Webページの表示領域にスクロールバーが重なってしまうのを防ぐために以下のJavaScriptを埋め込む。
                await WebView_Main.InvokeScriptAsync("eval", new string[] { SetScrollbarScript });
            }
            catch (Exception e)
            {
                WebView_Main.NavigateToString("Offline.");
            }
        }


        string SetScrollbarScript = @"
            function setScrollbar()
            {
                document.body.style.msOverflowStyle='scrollbar';   
            } 
            setScrollbar();";
    }
}

