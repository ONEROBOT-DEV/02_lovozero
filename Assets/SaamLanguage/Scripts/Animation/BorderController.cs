using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _borders;

    void Start()
    {
        var parent = transform.parent;
        int index = Enumerable.Range(0, parent.childCount).First(childIndex => parent.GetChild(childIndex) == transform);
        _borders[index % _borders.Count].SetActive(true);
    }
}
