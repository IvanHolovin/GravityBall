using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    private Transform target;
    private Material _material;
    private ChangeColorScript color;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        color = GetComponent<ChangeColorScript>();
        ChangeColor();
    }

    private void Start()
    {
        target = FindObjectOfType<WallFollowingEnemy>().transform;
    }

    void Update()
        {
            transform.position = new Vector3(target.transform.position.x, 0f,
                10f);
        }

        void OnEnable()
        {
            Gravity.GravityStateChange += ChangeColor;
        }

        void OnDestroy()
        {
            Gravity.GravityStateChange -= ChangeColor;
        }

        void ChangeColor()
        {
            if (Gravity.GravityState)
            {
                color.SetColorBlack(_material);
            }
            else
            {
                color.SetColorWhite(_material);
            }
        }
    }
    



