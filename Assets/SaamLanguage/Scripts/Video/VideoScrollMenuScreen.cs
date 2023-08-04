using Runtime.Menu;
using Runtime.Menu.MainMenu;
using Sirenix.OdinInspector;
using UnityEngine;

public class VideoScrollMenuScreen : MenuScreen
{
    [SceneObjectsOnly, Required]
    [SerializeField] private VideoMenuScreen _videoMenuScreen;
    [ChildGameObjectsOnly, Required]
    [SerializeField] private VideoController _controller;
    [SceneObjectsOnly, Required]
    [SerializeField] private MainMenuManager _mainMenuManager;

    private void Awake()
    {
        _controller.SetVideoScrollScreen(this);
    }

    public void SwitchToVideo(VideoData data)
    {
        _mainMenuManager.OpenScreen(_videoMenuScreen);
        _videoMenuScreen.SetVideo(data);
    }
}
