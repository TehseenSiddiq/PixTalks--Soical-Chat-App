using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUpEffect : MonoBehaviour
{
    public Ease easeIn = Ease.InBack;
    public Ease easeOut = Ease.OutBack;
    [SerializeField]
    private float duration = 0.2f;
    public float delay = 3f;
    public void Play()
    {
        transform.DOScale(1, duration).SetEase(easeOut);
        this.Wait(delay, () => transform.DOScale(0, duration).SetEase(easeIn));
    }
}
