using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TestAnimations : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;

    [ShowInInspector]
    private Color v0 => _textMesh.textInfo.meshInfo[_textMesh.textInfo.characterInfo[0].materialReferenceIndex].colors32[_textMesh.textInfo.characterInfo[0].vertexIndex + 0];

    [ShowInInspector]
    private Color v1 => _textMesh.textInfo.meshInfo[_textMesh.textInfo.characterInfo[1].materialReferenceIndex].colors32[_textMesh.textInfo.characterInfo[1].vertexIndex + 1];

    [ShowInInspector]
    private Color v2 => _textMesh.textInfo.meshInfo[_textMesh.textInfo.characterInfo[2].materialReferenceIndex].colors32[_textMesh.textInfo.characterInfo[2].vertexIndex + 2];

    [ShowInInspector]
    private Color v3 => _textMesh.textInfo.meshInfo[_textMesh.textInfo.characterInfo[3].materialReferenceIndex].colors32[_textMesh.textInfo.characterInfo[3].vertexIndex + 3];

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        ChangeFirstLetterCharacterColor();
    }

    void ChangeFirstLetterCharacterColor()
    {
        _textMesh.ForceMeshUpdate();

        //Color32 targetColor = Color.red;
        _textMesh.textInfo.meshInfo[_textMesh.textInfo.characterInfo[0].materialReferenceIndex].colors32[_textMesh.textInfo.characterInfo[0].vertexIndex + 0] = (Color32)Color.red;
        _textMesh.textInfo.meshInfo[_textMesh.textInfo.characterInfo[0].materialReferenceIndex].colors32[_textMesh.textInfo.characterInfo[0].vertexIndex + 1] = (Color32)Color.red;
        _textMesh.textInfo.meshInfo[_textMesh.textInfo.characterInfo[0].materialReferenceIndex].colors32[_textMesh.textInfo.characterInfo[0].vertexIndex + 2] = (Color32)Color.red;
        _textMesh.textInfo.meshInfo[_textMesh.textInfo.characterInfo[0].materialReferenceIndex].colors32[_textMesh.textInfo.characterInfo[0].vertexIndex + 3] = (Color32)Color.red;
        _textMesh.textInfo.meshInfo[0].mesh.colors32 = _textMesh.textInfo.meshInfo[0].colors32;
        _textMesh.UpdateVertexData();
        _textMesh.UpdateFontAsset();
        _textMesh.UpdateMeshPadding();
        _textMesh.SetAllDirty();

    }
}