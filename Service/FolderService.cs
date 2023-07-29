
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using winamptospotifyforms.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace winamptospotifyforms.Service
{
    public class FolderService
    {
        private readonly ILogger logger;

        public FolderService(ILogger logger)
        {
            this.logger = logger;
        }

        public List<string> GetMp3FileNames(ProcessFolder processFolder)
        {
            FileInfo[] filesInfoArray = new DirectoryInfo(processFolder.FilePath).GetFiles("*.mp3");
            List<string> fileNames = new List<string>();

            if (filesInfoArray.Length > 0)
            {
                foreach (var file in filesInfoArray)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.Name);
                    processFolder.IsArtistExistInFolderPath = fileName.ToLower().Contains(processFolder.ArtistAlbumName.ToLower());
                    if (!processFolder.ArtistAlbumName.ToLower().Any(x => char.IsNumber(x)))
                    {
                        fileName = new string(fileName.Where(Char.IsLetter).ToArray());
                    }
                    fileName = fileName.ToLower().Replace(processFolder.ArtistAlbumName.ToLower(), "", StringComparison.InvariantCultureIgnoreCase);
                    fileName = fileName.TrimStart();
                    fileName = fileName.TrimEnd();
                    fileNames.Add(fileName);
                }
            }
            else
            {
                logger.Error($"Cannot find any file in {processFolder.FilePath}");
                throw new FileNotFoundException($"Cannot find any file in {processFolder.FilePath}");
            }
            return fileNames;
        }
    }
}
