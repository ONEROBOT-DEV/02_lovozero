using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Gallery
{
    public static class GalleryLoadHelper
    {
        private const string ADDITIVE_GALLERY_FOLDER_PATH = "/Дата/Галерея/";


        public static string GetNameFromPath(string path)
        {
            var file = new FileInfo(path);
            return file.Name;
        }

        public static string[] GetImagesFromFolder()
        {
            string path = ADDITIVE_GALLERY_FOLDER_PATH;

            var dir = new DirectoryInfo(path);
            var files = new List<FileInfo>(dir.GetFiles());
            files = files.OrderBy(x => x.Name.Substring(0, 3)).ToList();
            string[] result = new string[files.Count];

            for (int index = 0; index < files.Count; index++)
            {
                result[index] = files[index].FullName;
            }

            return result;
        }

        public static IEnumerator LoadImages(string path, Action<Texture2D> onLoad)
        {
            var request = UnityWebRequestTexture.GetTexture(path);

            yield return request.SendWebRequest();

            if (!request.isHttpError && !request.isNetworkError)
            {
                var texture = DownloadHandlerTexture.GetContent(request);
                onLoad?.Invoke(texture);
            }
            else
            {
                Debug.LogErrorFormat("error request [{0}, {1}]", path, request.error);
                onLoad?.Invoke(null);
            }

            request.Dispose();
        }
    }
}
