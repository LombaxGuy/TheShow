using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class UpdateOnPlayer : MonoBehaviour
{
    [SerializeField]
    private AntiAliasingSettings aASettings;

    private Antialiasing playerAA;

    [SerializeField]
    private string nameOfFirstLevel = "TestMap";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerAA == null)
        {
            if (SceneManager.GetActiveScene().name == nameOfFirstLevel)
            {
                playerAA = Camera.main.GetComponent<Antialiasing>();
            }
        }
        else
        {
            switch (aASettings.CurrentType)
            {
                case AAType.Disabled:
                    if (playerAA.enabled)
                    {
                        playerAA.enabled = false;
                    }
                    break;

                case AAType.SSAA:
                    if (playerAA.mode != AAMode.SSAA)
                    {
                        playerAA.enabled = true;
                        playerAA.mode = AAMode.SSAA;
                    }
                    break;

                case AAType.NFAA:
                    if (playerAA.mode != AAMode.NFAA)
                    {
                        playerAA.enabled = true;
                        playerAA.mode = AAMode.NFAA;
                    }
                    break;

                case AAType.FXAA:
                    if (playerAA.mode != AAMode.FXAA3Console)
                    {
                        playerAA.enabled = true;
                        playerAA.mode = AAMode.FXAA3Console;
                    }
                    break;

                case AAType.DLAA:
                    if (playerAA.mode != AAMode.DLAA)
                    {
                        playerAA.enabled = true;
                        playerAA.mode = AAMode.DLAA;
                    }
                    break;
            }
        }
    }
}
