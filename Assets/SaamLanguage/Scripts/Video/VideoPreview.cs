using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPreview : MonoBehaviour
{
    [Required]
    [SerializeField] private TextMeshProUGUI _videoNameText;
    [Required]
    [SerializeField] private Button _playButton;
    [Required]
    [SerializeField] private RawImage _screen;
    [Required]
    [SerializeField] private VideoPlayer _videoPlayer;
    [Tooltip("С этого процента видео будет взято превью")]
    [Range(0f, 100f)]
    [SerializeField] private float _previewFramePercentage = 10f;

    public event Action<VideoData> Clicked;

    private VideoData _currentVideoData;

    public VideoData CurrentVideoData => _currentVideoData;

    private void OnEnable()
    {
        _videoPlayer.Prepare();
        _videoPlayer.prepareCompleted += DisplayPreview;
        _videoPlayer.Play();
        _videoPlayer.Pause();
    }

    private void OnDisable()
    {
        _videoPlayer.prepareCompleted -= DisplayPreview;

    }

    public bool SetupPreview(VideoData videoData)
    {
        _currentVideoData = videoData;
        _videoPlayer.url = videoData.VideoPath;
        _videoPlayer.targetTexture = videoData.RenderTexture;
        _screen.texture = videoData.RenderTexture;

        _videoNameText.text = videoData.VideoName.Normalize().ReplaceHats();
        return true;
    }

    public void DisplayPreview(VideoPlayer source)
    {
        source.time = _previewFramePercentage * source.length / 100f;
    }

    public void OnClick()
    {
        Clicked?.Invoke(_currentVideoData);
    }
}
