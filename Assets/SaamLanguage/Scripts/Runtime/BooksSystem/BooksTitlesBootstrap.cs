using System.Collections.Generic;
using System.IO;
using System.Linq;
using Runtime.Menu;
using UnityEngine;

namespace Runtime.BooksSystem
{
    public class BooksTitlesBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        [SerializeField] private BookTitleView _titlePrefab;

        [SerializeField] private MenuScreen _readingScreen;

        [SerializeField] private BaseMenuManager _manager;

        [SerializeField] private ReadingPanelManager _readingPanelManager;

        private List<string> _titles = new();

        private static string DataPath => Directory.GetCurrentDirectory() + "/Дата/Книги";

        private void Awake()
        {
            LoadTitles();
            CreateView();
        }

        private void LoadTitles()
        {
            _titles = Directory.GetDirectories(DataPath).ToList();

            for (int i = 0; i < _titles.Count; i++)
                _titles[i] = _titles[i].Substring(DataPath.Length + 1);
        }

        private void CreateView()
        {
            foreach (string title in _titles)
            {
                var instance = Instantiate(_titlePrefab, _container);
                instance.Init(title, _readingScreen, _manager, _readingPanelManager);
            }
        }
    }
}
