using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class VideoLoader : MonoBehaviour
{
    [Required]
    [SerializeField] private string _videoFolderPath;
    [Required]
    [SerializeField] private RenderTexture _videoTexture;

    private List<VideoData> _videoData;

    private static readonly List<string> _supportedVideoFormats = new()
    {
        ".asf",
        ".avi",
        ".dv",
        ".m4v",
        ".mov",
        ".mp4",
        ".mpg",
        ".mpeg",
        ".ogv",
        ".vp8",
        ".webm",
        ".wmv"
    };

    public IEnumerable<VideoData> Videos => _videoData;

    private void Awake()
    {
        if (_videoFolderPath.Length != 0 && _videoFolderPath[0] != '/')
        {
            _videoFolderPath = "\\" + _videoFolderPath;
        }
        PrepareTextures();
    }

    public void PrepareTextures()
    {
        string path = Directory.GetCurrentDirectory() + _videoFolderPath;
        if (!Directory.Exists(path))
            throw new ArgumentException(path + " doesn't exists");
        var files = Directory.GetFiles(path).Where(file => _supportedVideoFormats.Contains(Path.GetExtension(file))).OrderBy(file => Path.GetFileName(file)).ToList();
        _videoData = new List<VideoData>(files.Count);
        for (int i = 0; i < files.Count; i++)
        {
            _videoData.Add(new VideoData(files[i], CopyTexture(_videoTexture)));
        }
    }

    public static RenderTexture CopyTexture(RenderTexture texture)
    {
        var textureCopy = new RenderTexture(texture);
        Graphics.Blit(texture, textureCopy);
        return textureCopy;
    }
}
