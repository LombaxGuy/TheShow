using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberBoxToSliderValue : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private InputField numberBox;

    [SerializeField]
    private int minimumValue = 0;

    [SerializeField]
    private int maxumumValue = 100;

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

        value = value > maxumumValue ? (maxumumValue) : (value);

        value = value < minimumValue ? (minimumValue) : (value);

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
