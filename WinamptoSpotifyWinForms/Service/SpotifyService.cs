using Serilog;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using winamptospotifyforms.Models;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using winamptospotifyforms.Extensions;
using System.Collections.Specialized;

namespace winamptospotifyforms.Service
{
    public class SpotifyService : ISpotifyService
    {
        private readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private readonly ILogger logger;

        public SpotifyService(ILogger logger)
        {
            this.logger = logger;
        }        

        public async Task<string> GetAccessToken(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    logger.Information(Environment.NewLine + "Your basic bearer: " + Convert.ToBase64String(Encoding.ASCII.GetBytes(appSettings["ClientId"] + ":" + appSettings["SecretId"])));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(appSettings["ClientId"] + ":" + appSettings["SecretId"])));

                    FormUrlEncodedContent formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("code", code),
                        new KeyValuePair<string, string>("redirect_uri", appSettings["ForgeCallbackUrl"]),
                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    });

                    var result = await client.PostAsync(appSettings["TokenUrl"], formContent);
                    var content = await result.Content.ReadAsStringAsync();
                    var spotifyAuth = JsonSerializer.Deserialize<SpotifyApiResponse.AccessToken>(content);
                    return spotifyAuth.access_token;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }

        public async Task<string> CreatePlayList(ProcessFolder processFolder)
        {

            string playlistId = string.Empty;
            var stringPayload = new
            {
                name = processFolder.ArtistAlbumName,
                description = processFolder.ArtistAlbumName
            };
            var bodyPayload = new StringContent(JsonSerializer.Serialize(stringPayload), Encoding.UTF8, "application/json");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + processFolder.AccessToken);
                    var result = await client.PostAsync(appSettings["PlaylistBaseUrl"].Replace("{UserId}", appSettings["UserId"]), bodyPayload);
                    var content = await result.Content.ReadAsStringAsync();
                    var playlist = JsonSerializer.Deserialize<SpotifyApiResponse.PlayList>(content);
                    playlistId = playlist.id;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }

            logger.Information($"{processFolder.ArtistAlbumName} created successfully");
            return playlistId;
        }

        public string GetAuthorizationURL()
        {
            Dictionary<string, string> qb = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", ConfigurationManager.AppSettings["ClientId"]},
                { "scope", appSettings["ForgeScope"]},
                { "redirect_uri", appSettings["ForgeCallbackUrl"]}
            };

            return appSettings["AuthorizationUrl"] + qb.BuildQueryString();
        }

        public async Task<Dictionary<string, string>> GetTrackUri(ProcessFolder processFolder)
        {
            Dictionary<string, string> trackInfoDict = new Dictionary<string, string>();
            string artistOrAlbumName = processFolder.FilePath.Split('\\')[processFolder.FilePath.Split('\\').Length - 1];
            processFolder.ArtistAlbumName = artistOrAlbumName;
            List<string> fileNamesList = new FolderService(logger).GetMp3FileNames(processFolder);

            if (fileNamesList.Count > 0)
            {
                foreach (var qb in from item in fileNamesList
                                   let qb = BuildQueryForTrackAddition(artistOrAlbumName, item)
                                   select qb)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + processFolder.AccessToken);
                        var trackUrl = appSettings["TrackSearchBaseUrl"] + qb.ToString();
                        var result = await client.GetAsync(trackUrl);
                        if (result.IsSuccessStatusCode)
                        {
                            var content = await result.Content.ReadAsStringAsync();
                            var results = JsonSerializer.Deserialize<SpotifyApiResponse.RootObject>(content);
                            var tracks = results.tracks;
                            if (tracks.items.Count > 0)
                            {
                                trackInfoDict.TryAdd(tracks.items[0].uri, tracks.items[0].name);
                                logger.Information($"Track {tracks.items[0].name} found.");
                            }
                        }                       
                    }
                }
                return trackInfoDict;
            }
            return null;
        }

        private static string BuildQueryForTrackAddition(string artist, string item)
        {

            Dictionary<string, string> queryParameters = new Dictionary<string, string>
            {
                { "q", item + $" artist:{artist}" },
                { "type", "track" },
                { "limit", "1" }
            };

            return queryParameters.BuildQueryString();
        }

        public async Task<PlaylistSummary> ProcessFolder(string folderPath, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(folderPath)) throw new ArgumentNullException(nameof(folderPath));
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            try
            {
                string artistAndOrAlbum = folderPath.Split('\\').Select(s => s).Last();
                ProcessFolder processFolder = new ProcessFolder(accessToken, folderPath, artistAndOrAlbum);
                processFolder.PlaylistId = await CreatePlayList(processFolder);
                processFolder.TracksInfo = await GetTrackUriAndNames(processFolder);
                await AddTracksToPlaylistOnSpotify(processFolder);
                string addedTracks = String.Join(",", processFolder.TracksInfo.TrackName.Split(',', StringSplitOptions.RemoveEmptyEntries));

                logger.Information($"{processFolder.FilePath} is processed.");
                logger.Information($"{artistAndOrAlbum} album created successfully.Tracks added: {addedTracks}");

                return new PlaylistSummary
                {
                    AlbumName = artistAndOrAlbum,
                    TracksAdded = addedTracks,
                };

            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
                throw;
            }
        }
        private async Task AddTracksToPlaylistOnSpotify(ProcessFolder folderOperation)
        {
            Dictionary<string, string> queryParamaters = new Dictionary<string, string>
            {
                { "uris", folderOperation.TracksInfo.TrackUri }
            };

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + folderOperation.AccessToken);
                await client.PostAsync(appSettings["PlaylistAddTrackBaseUrl"].Replace("{playlist_id}", folderOperation.PlaylistId) + queryParamaters.BuildQueryString(), null);
            }

            await Task.Yield();
        }

        private async Task<TrackInfo> GetTrackUriAndNames(ProcessFolder processFolder)
        {
            Dictionary<string, string> trackInfoDict = await GetTrackUri(processFolder);
            return new TrackInfo(string.Join(",", trackInfoDict.Keys), string.Join(",", trackInfoDict.Values));
        }
    }
}
