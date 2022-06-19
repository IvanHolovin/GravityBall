using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class ChangeColorScript : MonoBehaviour
{
    public void SetColorBlack(Material material)
    {
      material.DOColor(Color.black,0.3f);
    }

    public void SetColorWhite(Material material)
    {
        material.DOColor(Color.white,0.3f);
    }
    
}
