using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject chasingWall;
    private Rigidbody rb;
    private Material _material;
    private ChangeColorScript color;
    private Vector3 startDistance;
    private Vector3 startPosition;
    private float currentSpeed = Const.Speed;
    private bool checkPlayState = true;
    

    private void Start()
    {
        ScoreManager.scoreReached += AddSpeed;
        Gravity.GravityStateChange += ChangeColor;
        GameStateDispatcher.Instance.AddListener(CheckOnRestart);
        GameStateDispatcher.Instance.AddListener(CheckOnPlay);
        
        startPosition = Const.PlayerStartPosition;
        startDistance = transform.position - chasingWall.transform.position;
        rb = GetComponent<Rigidbody>();
        _material = GetComponent<MeshRenderer>().material;
        color = GetComponent<ChangeColorScript>();
        ChangeColor();
    }

    private void OnEnable()
    {
        
    }

    private void OnDestroy()
    {
        ScoreManager.scoreReached -= AddSpeed;
        Gravity.GravityStateChange -= ChangeColor;
        GameStateDispatcher.Instance.RemoveListener(CheckOnRestart);
        GameStateDispatcher.Instance.RemoveListener(CheckOnPlay);

    }

    

    void FixedUpdate()
        {
            if (checkPlayState)
            {
                if ((transform.position.x - chasingWall.transform.position.x) > startDistance.x)
                {
                    currentSpeed = Const.Speed;
                }

                rb.transform.Translate(new Vector3(1, 0, 0) * currentSpeed * Time.deltaTime);
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
            color.SetColorWhite(_material); 
        }
        else
        {
            color.SetColorBlack(_material);
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
            checkPlayState = true;
        }
        else
        {
            checkPlayState = false;
        }
        
    }
    private void CheckOnRestart(GameState gameState)
    {
        if (gameState == GameState.Restart)
        {
            transform.position = startPosition;
        }
        else if (gameState == GameState.WatchAD)
        {
            transform.position = startDistance + chasingWall.transform.position;
        }
    }
    
    private void AddSpeed()
    {
        currentSpeed = Const.Speed + 1f;
    }

    public void SetFollowingWall(GameObject followingWallGameObject)
    {
        chasingWall = followingWallGameObject;
    }
}
