using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class UpdateAA : MonoBehaviour
{
    private AntiAliasingSettings aASettings;
    private Antialiasing aAComponent;

    private void Awake()
    {
        // Sets the fields
        aAComponent = GetComponent<Antialiasing>();
        aASettings = GameObject.Find("WorldManager").GetComponentInChildren<AntiAliasingSettings>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Switches on the currentType
        switch (aASettings.CurrentType)
        {
            case AAType.Disabled:
                if (aAComponent.enabled)
                {
                    aAComponent.enabled = false;
                }
                break;

            case AAType.SSAA:
                if (aAComponent.mode != AAMode.SSAA)
                {
                    aAComponent.enabled = true;
                    aAComponent.mode = AAMode.SSAA;
                }
                break;

            case AAType.NFAA:
                if (aAComponent.mode != AAMode.NFAA)
                {
                    aAComponent.enabled = true;
                    aAComponent.mode = AAMode.NFAA;
                }
                break;

            case AAType.FXAA:
                if (aAComponent.mode != AAMode.FXAA3Console)
                {
                    aAComponent.enabled = true;
                    aAComponent.mode = AAMode.FXAA3Console;
                }
                break;

            case AAType.DLAA:
                if (aAComponent.mode != AAMode.DLAA)
                {
                    aAComponent.enabled = true;
                    aAComponent.mode = AAMode.DLAA;
                }
                break;
        }
    }
}
