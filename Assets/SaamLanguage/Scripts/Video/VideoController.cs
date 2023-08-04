using Sirenix.OdinInspector;
using UnityEngine;

public class VideoController : MonoBehaviour
{
    [Required]
    [SerializeField] private VideoLoader _loader;
    [Required]
    [SerializeField] private Transform _videoContainer;
    [AssetsOnly, Required]
    [SerializeField] private VideoPreview _videoPreviewPrefab;
    
    private VideoScrollMenuScreen _videoScrollMenuScreen;

    private void Start()
    {
        InstantiateVideos();
    }

    public void SetVideoScrollScreen(VideoScrollMenuScreen screen)
    {
        _videoScrollMenuScreen = screen;
    }

    private void InstantiateVideos()
    {
        foreach (VideoData video in _loader.Videos)
        {
            var preview = Instantiate(_videoPreviewPrefab, _videoContainer);
            preview.Clicked += _videoScrollMenuScreen.SwitchToVideo;
            preview.SetupPreview(video);
        }
    }

    
}
