using Sirenix.OdinInspector;
using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _videoNameText;
    [SerializeField] private GameObject _playButtonIcon;
    [SerializeField] private Button _playButton;
    [SerializeField] private RawImage _screen;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private RenderTexture _renderTexture;

    [ShowInInspector]
    private bool IsPaused => _videoPlayer.isPaused;

    [ShowInInspector]
    private bool IsPlaying => _videoPlayer.isPlaying;

    [ShowInInspector]
    private double Time => _videoPlayer.time;

    [ShowInInspector]
    private double Length => _videoPlayer.length;

    private void Awake()
    {
        var renderTextureCopy = VideoLoader.CopyTexture(_renderTexture);
        _videoPlayer.targetTexture = renderTextureCopy;
        _screen.texture = renderTextureCopy;
    }

    private void OnDisable()
    {
        PauseVideo();
        _videoPlayer.Stop();
        ResetTexture();
    }

    private void Update()
    {
        if (_videoPlayer.isPaused)
        {
            PauseVideo();
        }
    }

    private void ResetTexture()
    {
        RenderTexture.active = _videoPlayer.targetTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = null;
    }

    public bool SetupVideo(string path)
    {
        _videoPlayer.url = path;

        _videoPlayer.frame = 0;
        _videoPlayer.Prepare();
        _videoPlayer.Play();
        _videoPlayer.Pause();
        _videoNameText.text = Path.GetFileNameWithoutExtension(path).Normalize().ReplaceHats();
        return true;
    }

    public void ToggleVideo()
    {
        if (_videoPlayer.isPlaying)
        {
            PauseVideo();
        } 
        else
        {
            PlayVideo();
        }
    }

    public void PlayVideo()
    {
        _playButtonIcon.SetActive(false);
        _videoPlayer.Play();
    }

    public void PauseVideo()
    {
        _playButtonIcon.SetActive(true);
        _videoPlayer.Pause();
    }

    public void OnClick()
    {
        ToggleVideo();
    }
}
