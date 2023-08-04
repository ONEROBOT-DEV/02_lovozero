using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Gallery.ImagePageView
{
    public class GalleryPageDataController : MonoBehaviour
    {
        [SerializeField] private ImageViewController _imageViewController;
        private string[] _images;

        public void InitPanel()
        {
            _imageViewController.GetImageFromIndex = GetTextureFromIndex;
            _imageViewController.GetImageDescriptionFromIndex = GetDescriptionFromIndex;
            _imageViewController.GetImageCount = () =>
            {
                if (_images == null)
                    InitImages();
                return _images.Length;
            };
        }

        private void InitImages()
        {
            _images = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Дата/Галерея/");
        }

        private void GetTextureFromIndex(int index, Action<Texture2D> onLoad)
        { if (_images == null)
                InitImages();
            StartCoroutine(GalleryLoadHelper.LoadImages(_images[CalculateIndex(index)], onLoad));
        }

        private string GetDescriptionFromIndex(int index)
        {
            string[] description = GalleryLoadHelper.GetNameFromPath(_images[CalculateIndex(index)]).Substring(4)
                .Split('.');
            var str = new StringBuilder();

            for (int indexSplit = 0; indexSplit < description.Length - 1; indexSplit++)
            {
                str.Append(description[indexSplit]);
                str.Append('.');
            }

            return str.ToString();
        }

        private int CalculateIndex(int index)
        {
            if(_images == null)
                InitImages();
            if (index >= _images.Length) index %= _images.Length;
            else if (index < 0) index = (_images.Length + index % _images.Length) % _images.Length;

            return index;
        }
    }
}
