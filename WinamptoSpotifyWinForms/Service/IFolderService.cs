using System.Collections.Generic;
using WinamptoSpotifyWinForms.Models;

namespace WinamptoSpotifyWinForms.Service
{
    public interface IFolderService
    {
        List<string> GetMp3FileNames(ProcessFolder processFolder);        
    }
}
