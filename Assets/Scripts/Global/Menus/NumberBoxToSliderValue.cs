using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberBoxToSliderValue : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private InputField numberBox;

    private void OnEnable()
    {
        EventManager.OnLoadPref += OnLoadPref;
    }

    private void OnDisable()
    {
        EventManager.OnLoadPref -= OnLoadPref;
    }

    public void UpdateSlider()
    {
        int value = 0;
        int.TryParse(numberBox.text, out value);

        value = value > 100 ? (100) : (value);

        value = value < 0 ? (0) : (value);

        slider.value = value;
        numberBox.text = value.ToString();
    }

    public void UpdateNumberBox()
    {
        int value = (int)slider.value;
        numberBox.text = value.ToString();
    }

    private void OnLoadPref()
    {
        UpdateNumberBox();
    }
}
