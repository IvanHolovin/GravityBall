using DG.Tweening;
using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    [SerializeField] private KillType _killType;
    
    private float _previosPositionX;
    void OnEnable()
    {
        CheckYLocationAndStartLoop();
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
        if (player != null && player.CheckOnKill(_killType))
        {
            DeathDispatcher.Instance.ActionWasLoaded(_killType);
            transform.gameObject.SetActive(false);
        }
    }
}
