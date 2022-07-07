using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    private Transform _targetWall;
    private Material _material;
    private ChangeColorScript _color;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _color = GetComponent<ChangeColorScript>();
        ChangeColor();
    }

    private void Start()
    {
        _targetWall = FindObjectOfType<WallFollowingEnemy>().transform;
    }

    void Update()
        {
            transform.position = new Vector3(_targetWall.transform.position.x, 0f,
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
                _color.SetColorBlack(_material);
            }
            else
            {
                _color.SetColorWhite(_material);
            }
        }
    }
    



