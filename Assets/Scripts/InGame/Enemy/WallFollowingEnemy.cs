using UnityEngine;

public class WallFollowingEnemy : MonoBehaviour
{
    [SerializeField] private KillType _killType;

    private void Awake()
    {
        GameStateDispatcher.Instance.AddListener(CheckOnRestart);
    }
    private void OnDestroy()
    {
        GameStateDispatcher.Instance.RemoveListener(CheckOnRestart);
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(1,0,0) * Const.SPEED * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.CheckOnKill(_killType))
        {
            DeathDispatcher.Instance.ActionWasLoaded(_killType);
        }
    }
    
    private void CheckOnRestart(GameState gameState)
    {
        if (gameState == GameState.Restart)
        {
            transform.position = Const.WallStartPosition;
        }
    }
}
