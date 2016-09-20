using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class UpdateOnPlayer : MonoBehaviour
{
    [SerializeField]
    private AntiAliasingSettings aASettings;

    [SerializeField]
    private FOVSetting fovSetting;

    [SerializeField]
    private HeadbobSetting headbobSetting;

    private Animator playerAnimator;
    private Antialiasing playerAA;

    private float currentHeadbobAmount = 0;
    private int indexOfHeadbobLayer;

    [SerializeField]
    private string nameOfFirstLevel = "TestMap";

    // Update is called once per frame
    private void Update()
    {
        if (playerAA == null || playerAnimator == null)
        {
            if (SceneManager.GetActiveScene().name == nameOfFirstLevel)
            {
                playerAA = Camera.main.GetComponent<Antialiasing>();
                playerAnimator = Camera.main.GetComponentInParent<Animator>();

                indexOfHeadbobLayer = playerAnimator.GetLayerIndex("Headbob Layer");
                currentHeadbobAmount = playerAnimator.GetLayerWeight(indexOfHeadbobLayer) * 100;
            }
        }
        else
        {
            if (Camera.main.fieldOfView != fovSetting.CurrentFov)
            {
                Camera.main.fieldOfView = fovSetting.CurrentFov;
            }

            if (currentHeadbobAmount != headbobSetting.CurrentValue)
            {
                float setValue = headbobSetting.CurrentValue == 0 ? (0.01f) : (headbobSetting.CurrentValue / 100);

                playerAnimator.SetLayerWeight(indexOfHeadbobLayer, setValue);
                currentHeadbobAmount = playerAnimator.GetLayerWeight(indexOfHeadbobLayer) * 100;
            }

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
