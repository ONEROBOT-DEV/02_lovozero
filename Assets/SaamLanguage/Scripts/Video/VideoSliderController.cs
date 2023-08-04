using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSliderController : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Slider _slider;

    private bool _selected;

    private void Update()
    {
        UpdateSliderValue();
    }

    private void OnDisable()
    {
        ResetSlider();
    }

    private void ResetSlider()
    {
        _slider.value = 0;
    }

    private void UpdateSliderValue()
    {
        if (_selected)
            return;
        if (_videoPlayer.length == 0)
            return;
        _slider.value = (float)(_videoPlayer.time / _videoPlayer.length);
    }

    private void RewindVideo(float value)
    {
        double targetTime = value * _videoPlayer.length;
        _videoPlayer.time = targetTime;
    }

    public void OnSelect()
    {
        _selected = true;
    }

    public void OnDeselect()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        RewindVideo(_slider.value);
        yield return new WaitForSeconds(0.1f);
        _selected = false;
    }
}
