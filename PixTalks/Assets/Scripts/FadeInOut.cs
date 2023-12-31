using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeInOut : MonoBehaviour
{
    public Ease easeIn = Ease.InBack;
    public Ease easeOut = Ease.OutBack;
    [SerializeField]
    private float duration = 0.5f;
    private void OnEnable()
    {
        transform.DOScale(1, duration).SetEase(easeOut);
    }
    public void Hide()
    {
        transform.DOScale(0, duration).SetEase(easeIn);
        this.Wait(duration + 0.5f, () =>  gameObject.SetActive(false));
    }
}
