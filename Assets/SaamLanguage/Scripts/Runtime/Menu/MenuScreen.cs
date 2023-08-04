using System;
using fidt17.UnityValidationModule.Runtime.Attributes.FieldAttributes;
using UnityEngine;

namespace Runtime.Menu
{
    public class MenuScreen : MonoBehaviour
    {
        [SerializeField] [NotNullValidation] private GameObject _graphicGm;
        public Action OnScreenDisable;

        public void Enable()
        {
            _graphicGm.SetActive(true);
        }

        public void Disable()
        {
            _graphicGm.SetActive(false);
            OnScreenDisable?.Invoke();
        }
    }
}
