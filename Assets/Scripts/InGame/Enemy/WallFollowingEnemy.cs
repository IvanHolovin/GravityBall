using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFollowingEnemy : MonoBehaviour
{
    [SerializeField] private KillType killType;

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
        transform.Translate(new Vector3(1,0,0) * Const.Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.CheckOnKill(killType))
        {
            DeathDispatcher.Instance.ActionWasLoaded(killType);
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
