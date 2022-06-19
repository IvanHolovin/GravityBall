using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    void Start()
    {
        target = GameObject.FindObjectOfType<WallFollowingEnemy>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x + 11f, 1f,
            -10f);
    }
}
