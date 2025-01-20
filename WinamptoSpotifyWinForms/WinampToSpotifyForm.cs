using Serilog;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WinamptoSpotifyWinForms.Models;
using WinamptoSpotifyWinForms.Service;

namespace WinamptoSpotifyWinForms
{
    public partial class WinampToSpotify : Form
    {        
        private readonly SpotifyService spotifyService;        

        public WinampToSpotify(ILogger logger)
        {
            InitializeComponent();
            this.spotifyService = new SpotifyService(logger);            
        }

        private async Task GetAccessToken()
        {
            var authorizeRequest = spotifyService.GetAuthorizationURL();
            webView.Visible = true;
            webView.CoreWebView2.Navigate(authorizeRequest);

            webView.NavigationStarting += async (sender, e) =>
            {
                if (!e.Uri.StartsWith(ConfigurationManager.AppSettings["ForgeCallbackUrl"]))
                    return;
                var uri = new Uri(e.Uri);
                var code = HttpUtility.ParseQueryString(uri.Query).Get("code");
                var token = await spotifyService.GetAccessToken(code);
                accesstokenTxt.Text = token;
                MessageBox.Show("Access token successfully created.");
                getAccessTokenBtn.Hide();
                webView.Hide();
            };

            await Task.Yield();
        }

        /// <summary>Getting access token button event handler.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void getAccessToken_Click(object sender, EventArgs e)
        {
            await GetAccessToken();
        }

        /// <summary>Folder select click event handler method.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void selectButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFolderDialog.ShowDialog();
            resultTxt.Clear();
            resultTxt.Visible = true;
            resultTxt.Text = "Loading...";
            if (result == DialogResult.OK)
            {
                var folderName = new FileInfo(openFolderDialog.FileName).Directory.FullName;
                folderNameText.Text = folderName;
                string access_token = accesstokenTxt.Text;
                resultTxt.Text = await LoadResultsAsync(folderName, access_token);
            }
        }

        private Task<string> LoadResultsAsync(string folderName, string accessToken)
        {
            StringBuilder stringBuilder = new StringBuilder();
            return Task.Run(async () =>
            {
                PlaylistSummary playlistSummary = await spotifyService.ProcessFolder(folderName, accessToken);
                stringBuilder.Append($"{playlistSummary.AlbumName} album created successfully{Environment.NewLine}");
                stringBuilder.Append("Tracks added" + Environment.NewLine);
                foreach (var trackName in playlistSummary.TracksAdded.Split(","))
                {
                    stringBuilder.Append(trackName + Environment.NewLine);
                }
                return stringBuilder.ToString();
            });
        }
    }
}

