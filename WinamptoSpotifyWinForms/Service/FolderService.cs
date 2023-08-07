using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using winamptospotifyforms.Models;

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
                    fileName = NormalizeFileName(processFolder.ArtistAlbumName, fileName);
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

        private static string NormalizeFileName(string artistName, string fileName)
        {
            Regex reg = new Regex(@"[^\p{L}\p{N} ]");
            fileName = reg.Replace(fileName, String.Empty);
            fileName = Regex.Replace(fileName, @"[0-9]+", "");
            fileName = fileName.Replace(artistName, "");
            fileName = fileName.TrimStart();
            fileName = fileName.TrimEnd();
            return fileName;
        }
    }
}
