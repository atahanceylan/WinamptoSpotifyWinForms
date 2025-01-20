using System.Threading.Tasks;
using WinamptoSpotifyWinForms.Models;

namespace WinamptoSpotifyWinForms.Service
{
    public interface ISpotifyService
    {
        Task<string> GetAccessToken(string code);

        Task<PlaylistSummary> ProcessFolder(string folderPath, string accessToken);
    }
}
