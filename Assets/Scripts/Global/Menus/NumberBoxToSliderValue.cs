using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberBoxToSliderValue : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private InputField numberBox;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void UpdateSlider()
    {
        int value = 0;
        int.TryParse(numberBox.text, out value);

        value = value > 1 ? (1) : (value);

        slider.value = value;
    }

    public void UpdateNumberBox()
    {

    }
}
