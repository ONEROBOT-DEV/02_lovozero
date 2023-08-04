using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime
{
    public class SwipeHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        /// <summary>
        ///     arg1 = swipe up?
        /// </summary>
        public event Action<bool> OnVerticalSwipe;

        [SerializeField] private float swipeThreshold = 100f;

        private Vector2 _swipeStartPosition;

        public void OnDrag(PointerEventData eventData)
        {
            var swipeCurrentPosition = eventData.position;

            var difference = swipeCurrentPosition - _swipeStartPosition;

            if (Mathf.Abs(difference.y) >= swipeThreshold)
                OnVerticalSwipe?.Invoke(difference.y > 0);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _swipeStartPosition = Vector2.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _swipeStartPosition = eventData.position;
        }
    }
}
