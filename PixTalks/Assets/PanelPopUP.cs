using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelPopUP : MonoBehaviour
{
    public Ease easeOut = Ease.OutBack;
    public float delay = 0.2f;
    public void POP()
    {
        transform.DOScale(1, delay).SetEase(easeOut);
    }
    public void Cancel()
    {
        transform.DOScale(0, delay).SetEase(easeOut);
    }
}
