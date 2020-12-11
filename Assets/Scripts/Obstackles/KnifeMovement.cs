using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class KnifeMovement : MonoBehaviour
{
    private void OnEnable()
    {
        this.transform.DORotate(new Vector3(40, -25, 25), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
