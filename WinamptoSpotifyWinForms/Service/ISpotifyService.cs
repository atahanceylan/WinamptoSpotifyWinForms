using System.Threading.Tasks;
using winamptospotifyforms.Models;

namespace winamptospotifyforms.Service
{
    public interface ISpotifyService
    {
        Task<string> GetAccessToken(string code);

        Task<PlaylistSummary> ProcessFolder(string folderPath, string accessToken);
    }
}
