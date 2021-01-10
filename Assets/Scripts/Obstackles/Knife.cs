using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knife : Obstackle
{
    private void Start()
    {
        this.transform.DORotate(new Vector3(40, -25, 25), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
