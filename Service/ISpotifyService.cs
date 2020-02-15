using System.Threading.Tasks;
using winamptospotifyforms.Models;

namespace winamptospotifyforms.Service
{
    public interface ISpotifyService
    {
        /// <summary>
        /// Creates access token based on code created with Authorization Button
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<string> GetAccessToken(string code);

        /// <summary>
        /// Processes mp3 files for selected folder and creates playlist on Spotify
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<PlaylistSummary> ProcessFolder(string folderPath, string accessToken);
    }
}
