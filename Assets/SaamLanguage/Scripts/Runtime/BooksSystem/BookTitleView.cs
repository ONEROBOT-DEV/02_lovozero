using Runtime.Menu;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.BooksSystem
{
    public class BookTitleView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;

        public void Init(string title, MenuScreen readingScreen, BaseMenuManager manager,
            ReadingPanelManager readingPanelManager)
        {
            if (title.Contains("-"))
            {
                _text.text = title.Substring(0, title.LastIndexOf('-')).Normalize();
            }
            else
            {
                _text.text = title.Normalize(); 
            }
            _button.onClick.AddListener(() => manager.OpenScreen(readingScreen));
            _button.onClick.AddListener(() => readingPanelManager.Load(title));
        }

        public static string ReplaceUmlauts(string text)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 'и' && i != text.Length - 1 && text[i + 1] == (char)774)
                {
                    builder.Append("й");
                } 
                else
                {
                    builder.Append(text[i]);
                }
            }
            return builder.ToString();
        }
    }
}
