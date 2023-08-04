using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Menu
{
    public abstract class BaseMenuManager : MonoBehaviour
    {
        [SerializeField] private MenuScreen _startScreen;

        private readonly Stack<MenuScreen> _screens = new();


        public void Start()
        {
            _screens.Push(_startScreen);

            Init();
        }

        protected abstract void Init();

        public void OpenScreen(MenuScreen menuScreen)
        {
            _screens.Peek().Disable();
            _screens.Push(menuScreen);
            _screens.Peek().Enable();
        }

        public void Back()
        {
            _screens.Pop().Disable();
            _screens.Peek().Enable();
        }

        public void Home()
        {
            while (_screens.Count > 1)
                _screens.Pop().Disable();
            _screens.Peek().Enable();
        }
    }
}
