using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueManager : MonoBehaviour
{
    public TMP_Text text;
    public Slider slider;

    public void ChangeValue()
    {
        text.text = slider.value.ToString();
    }
}
