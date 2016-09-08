using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberBoxToSliderValue : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private InputField numberBox;

    public void UpdateSlider()
    {
        int value = 0;
        int.TryParse(numberBox.text, out value);

        value = value > 100 ? (100) : (value);

        slider.value = value;
        numberBox.text = value.ToString();
    }

    public void UpdateNumberBox()
    {
        int value = (int)slider.value;
        numberBox.text = value.ToString();
    }
}
