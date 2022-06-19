using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    [SerializeField] private KillType killType;
    private float previosPositionX;
    void OnEnable()
    {
        CheckYLocationAndStartLoop();
        Debug.Log("OnEnableEnemy");
    }

    private void OnDestroy()
    {
        DOTween.CompleteAll();
        DOTween.KillAll();
    }

    private void CheckYLocationAndStartLoop()
    {
        if (transform.localPosition.y > 1)
        {
            transform.DOLocalMoveY(transform.localPosition.y-2, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else if (transform.localPosition.y <= 1)
        {
            transform.DOLocalMoveY(transform.localPosition.y+2, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.CheckOnKill(killType))
        {
            DeathDispatcher.Instance.ActionWasLoaded(killType);
            transform.gameObject.SetActive(false);
        }
    }
}
