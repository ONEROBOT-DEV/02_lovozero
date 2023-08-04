using DG.Tweening;
using System;
using UnityEngine;

public class QuizAnimation : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private Ease _animationEase;

    public void Animate(Transform toMove, Direction direction)
    {
        Vector2 center = new Vector2(Screen.width * 0.5f, toMove.position.y);
        switch (direction)
        {
            case Direction.Off:
                AnimateFromTo(toMove, center, (Vector3)center + Screen.width * Vector3.left, () => toMove.gameObject.SetActive(false));
                break;
            case Direction.On:
                toMove.gameObject.SetActive(true);
                AnimateFromTo(toMove, (Vector3)center + Vector3.right * Screen.width, center, () => { });
                break;
            default:
                throw new NotImplementedException("Unconsidered case in switch case");
        }
    }

    private void AnimateFromTo(Transform toMove, Vector3 from, Vector3 to, Action OnComplete)
    {
        toMove.position = from;
        toMove.DOMove(to, _animationDuration).SetEase(_animationEase).OnComplete(() => OnComplete());
    }

    public enum Direction
    {
        On,
        Off
    }
}
