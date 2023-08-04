using System;
using System.IO;
using UnityEngine;
using Path = System.IO.Path;

[Serializable]
public struct VideoData
{
    private string _videoPath;
    private RenderTexture _renderTexture;

    public string VideoPath => _videoPath;

    public string VideoName => Path.GetFileNameWithoutExtension(_videoPath);

    public RenderTexture RenderTexture => _renderTexture;

    public VideoData(string videoPath, RenderTexture renderTexture)
    {
        _videoPath = videoPath;
        _renderTexture = renderTexture;
        ValidatePath(videoPath);
    }

    private void ValidatePath(string videoPath)
    {
        if (!File.Exists(videoPath))
            throw new System.ArgumentException($"{videoPath} не существует");
    }
}
