namespace WinamptoSpotifyWinForms.Models
{
    public class TrackInfo
    {
        /// <summary>Gets or sets track URI from Spotify Web Api.</summary>
        public string TrackUri { get; set; }

        /// <summary>Gets or sets track name.</summary>
        public string TrackName { get; set; }

        public TrackInfo(string trackUri, string trackName)
        {
            TrackUri = trackUri;
            TrackName = trackName;
        }

    }
}
