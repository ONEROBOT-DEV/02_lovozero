using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class QuizWinDisplay : MonoBehaviour
{
    [Required]
    [SerializeField] private TextMeshProUGUI _pointText;
    [Required]
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void OnEnable()
    {
        _pointText.text = "";
        _textMeshPro.text = "";
    }

    public void SetWinMessage(WinMessage winMessage, int points, int maxPoints)
    {
        _pointText.text = $"Очки: {points}/{maxPoints}\n";
        _textMeshPro.text = winMessage.Text;
    }
}
