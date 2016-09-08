using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VideoSettings : MonoBehaviour
{
    [SerializeField]
    private Dropdown resolutionDD;

    [SerializeField]
    private Dropdown textureQualityDD;

    [SerializeField]
    private Dropdown anisotropicFilteringDD;

    [SerializeField]
    private Dropdown shadowQualityDD;

    [SerializeField]
    private Dropdown displayMonitorDD;

    [SerializeField]
    private Dropdown antiAliasingQualityDD;

    [SerializeField]
    private Dropdown anitAliasingTypeDD;

    [SerializeField]
    private Toggle vSyncToggle;

    [SerializeField]
    private Toggle windowedToggle;

    [SerializeField]
    private Slider headbobAmountSlider;

    private int minimumResolutionWidth = 800;
    private int minimumResolutionHeight = 600;

    private void Awake()
    {
        //SetResolutions();
    }

    private void SetResolutions()
    {
        resolutionDD.ClearOptions();

        List<string> resolutions = new List<string>();

        foreach (var resolution in Screen.resolutions)
        {
            if (!(resolution.width < minimumResolutionWidth) && !(resolution.height < minimumResolutionHeight))
            {
                resolutions.Add(resolution.width + "x" + resolution.height + " " + resolution.refreshRate + "Hz");
            } 
        }

        resolutionDD.AddOptions(resolutions);
    }

    public void ApplySettings()
    {
        
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
