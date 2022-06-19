using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    
    private ChangeColorScript color;
    private List<MeshRenderer> childrenMesh = new List<MeshRenderer>();
    

    private void Awake()
    {
        color = GetComponent<ChangeColorScript>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<MeshRenderer>() != null)
                childrenMesh.Add(child.gameObject.GetComponent<MeshRenderer>());
           
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
            foreach (var part in childrenMesh)
            {
                color.SetColorWhite(part.material);
            }
        }
        else
        {
            foreach (var part in childrenMesh)
            {
                color.SetColorBlack(part.material);
            }
        }
    }

}
