using System;
using Runtime.Menu;
using SimpleTouchControl;
using UnityEngine;

namespace Gallery.ImagePageView
{
public class ImageViewController : MenuScreen
{
    [SerializeField] private PageImageController _pageImageController;
    [SerializeField] private GalleryPageDataController _galleryPageDataController;
    [SerializeField] private CirclesPageController _circlesPageController;
    
    [SerializeField] private InputManager _inputManager;
    public Action<int> OnChangeCurrentIndex;
    public Action<int> OnSwipeImage;

    public Action<int, Action<Texture2D>> GetImageFromIndex;
    public Func<int,string> GetImageDescriptionFromIndex;
    public Func<int> GetImageCount;
    
    private int _currentIndex;

    private void Awake()
    {
        InitPanel();
        
        
        _pageImageController.InitPanel();
        _galleryPageDataController.InitPanel();
        _circlesPageController.InitPanel();
    }

    private void OnEnable()
    {
        _pageImageController.Enable();
        _circlesPageController.Enable();
    }

    private void InitPanel()
    {
        _inputManager.onSwipeLeft += (_) => OnSwipeToDirection(1);
        _inputManager.onSwipeRight += (_) => OnSwipeToDirection(-1);
    }

    private void OnSwipeToDirection(int direction)
    {
        _currentIndex += direction;

        OnSwipeImage?.Invoke(direction);
    }

    private void OnChangeIndex()
    {
        OnChangeCurrentIndex?.Invoke(_currentIndex);
    }
}
    
}
