using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Runtime.BooksSystem
{
    public class ReadingPanelLoader
    {
        public void Load(string title, out List<string> paths)
        {
            string path = Directory.GetCurrentDirectory() + "/Дата/Книги/" + title;
            paths = new List<string>();

            var dir = new DirectoryInfo(path);
            var files = dir.GetFiles();
            var sortedFiles = files.OrderBy(r => r.Name).ToArray();
            sortedFiles = files
                .Where(file => !string.Equals(Path.GetExtension(file.Name), ".meta"))
                .OrderBy(r => int.Parse(Path.GetFileNameWithoutExtension(r.Name)))
                .ToArray();
            foreach (var sortedFile in sortedFiles)
                paths.Add(sortedFile.FullName);
        }

        public Texture2D LoadImageReadAllBytes(string path)
        {
            byte[] pngBytes = File.ReadAllBytes(path);
            var texture2D = new Texture2D(1, 1);
            texture2D.LoadImage(pngBytes);

            return texture2D;
        }
    }
}
