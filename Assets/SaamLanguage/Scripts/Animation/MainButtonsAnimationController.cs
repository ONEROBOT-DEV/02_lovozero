using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainButtonsAnimationController : MonoBehaviour
{
    [SerializeField] private float _startAnimationPause;
    [SerializeField] private float _pauseBetweenAnimations;
    [SerializeField] private List<MainButtonAnimation> _buttonAnimations;

    private void OnEnable()
    {
        StartCoroutine(AnimateButtons());
    }

    private IEnumerator AnimateButtons()
    {
        while (true)
        {
            yield return new WaitForSeconds(_startAnimationPause);
            foreach (var animation in _buttonAnimations)
            {
                animation.Animate();
                yield return new WaitForSeconds(_pauseBetweenAnimations);
            }
        }
    }
}
