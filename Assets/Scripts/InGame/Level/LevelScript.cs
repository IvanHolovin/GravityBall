using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    
    private ChangeColorScript _color;
    private List<MeshRenderer> _childrenMesh = new List<MeshRenderer>();
    

    private void Awake()
    {
        _color = GetComponent<ChangeColorScript>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<MeshRenderer>() != null)
                _childrenMesh.Add(child.gameObject.GetComponent<MeshRenderer>());
        }
        ChangeColor();
    }

    private void OnEnable()
    {
        Gravity.GravityStateChange += ChangeColor;
    }

    private void OnDestroy()
    {
        Gravity.GravityStateChange -= ChangeColor;
    }

    private void ChangeColor()
    {
        if (Gravity.GravityState)
        {
            foreach (var part in _childrenMesh)
            {
                _color.SetColorWhite(part.material);
            }
        }
        else
        {
            foreach (var part in _childrenMesh)
            {
                _color.SetColorBlack(part.material);
            }
        }
    }

}
