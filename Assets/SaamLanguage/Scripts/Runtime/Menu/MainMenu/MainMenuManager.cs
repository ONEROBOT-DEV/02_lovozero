using UnityEngine;

namespace Runtime.Menu.MainMenu
{
    public class MainMenuManager : BaseMenuManager
    {
        [SerializeField] private MenuScreen _galleryFolderScreen;
        [SerializeField] private MenuScreen _galleryImagePageScreen;

        public void OpenGalleryFolderView()
        {
            OpenScreen(_galleryFolderScreen);
        }
        
        public void OpenGalleryImagePage()
        {
            OpenScreen(_galleryImagePageScreen);
        }

        protected override void Init()
        {
        }

        #region CallStaffHere

        //Load scene

        //Exit

        //...

        #endregion
    }
}
