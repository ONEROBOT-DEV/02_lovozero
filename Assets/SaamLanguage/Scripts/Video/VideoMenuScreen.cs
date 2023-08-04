using UnityEngine;
using Runtime.Menu;
using Sirenix.OdinInspector;

public class VideoMenuScreen : MenuScreen
{
    [ChildGameObjectsOnly, Required]
    [SerializeField] private VideoDisplay _videoDisplay;

    public void SetVideo(VideoData video)
    {
        _videoDisplay.SetupVideo(video.VideoPath);
    }
}
