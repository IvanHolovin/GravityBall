using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject _chasingWall;
    private Rigidbody _playerRigidbody;
    private Material _material;
    private ChangeColorScript _color;
    private Vector3 _startDistance;
    private Vector3 _startPosition;
    private float _currentSpeed = Const.SPEED;
    private bool _checkPlayState = true;
    
    private void Start()
    {
        ScoreManager.ScoreReached += AddSpeed;
        Gravity.GravityStateChange += ChangeColor;
        GameStateDispatcher.Instance.AddListener(CheckOnRestart);
        GameStateDispatcher.Instance.AddListener(CheckOnPlay);
        
        _startPosition = Const.PlayerStartPosition;
        _startDistance = transform.position - _chasingWall.transform.position;
        _playerRigidbody = GetComponent<Rigidbody>();
        _material = GetComponent<MeshRenderer>().material;
        _color = GetComponent<ChangeColorScript>();
        ChangeColor();
    }

    private void OnDestroy()
    {
        ScoreManager.ScoreReached -= AddSpeed;
        Gravity.GravityStateChange -= ChangeColor;
        GameStateDispatcher.Instance.RemoveListener(CheckOnRestart);
        GameStateDispatcher.Instance.RemoveListener(CheckOnPlay);
    }

    

    void FixedUpdate()
        {
            if (_checkPlayState)
            {
                if ((transform.position.x - _chasingWall.transform.position.x) > _startDistance.x)
                {
                    _currentSpeed = Const.SPEED;
                }

                _playerRigidbody.transform.Translate(new Vector3(1, 0, 0) * _currentSpeed * Time.deltaTime);
                if (transform.position.y > 6f || transform.position.y < -4)
                {
                    DeathDispatcher.Instance.ActionWasLoaded(KillType.Out);
                }
            }
        }
    
    private void ChangeColor()
    {
        if (Gravity.GravityState)
        {
            _color.SetColorWhite(_material); 
        }
        else
        {
            _color.SetColorBlack(_material);
        }
    }

    public bool CheckOnKill(KillType killType)
    {
        return true;
    }
    private void CheckOnPlay(GameState gameState)
    {
        if (gameState == GameState.Play)
        {
            _checkPlayState = true;
        }
        else
        {
            _checkPlayState = false;
        }
    }
    
    private void CheckOnRestart(GameState gameState)
    {
        if (gameState == GameState.Restart)
        {
            transform.position = _startPosition;
        }
        else if (gameState == GameState.WatchAD)
        {
            transform.position = _startDistance + _chasingWall.transform.position;
        }
    }
    
    private void AddSpeed()
    {
        _currentSpeed = Const.SPEED + 1f;
    }

    public void SetFollowingWall(GameObject followingWallGameObject)
    {
        _chasingWall = followingWallGameObject;
    }
}
