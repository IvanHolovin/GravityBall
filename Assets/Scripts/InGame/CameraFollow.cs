using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    void Start()
    {
        _target = GameObject.FindObjectOfType<WallFollowingEnemy>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_target.transform.position.x + 10.8f, 1f,
            -10f);
    }
}
