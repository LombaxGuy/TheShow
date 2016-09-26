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

    [SerializeField]
    private string nameOfFirstLevel = "TestMap";

    private Animator playerAnimator;
    private Antialiasing playerAA;

    private float currentHeadbobAmount = 0;
    private int indexOfHeadbobLayer;

    // Update is called once per frame
    private void Update()
    {
        // If playerAA or playerAnimator is null...
        if (playerAA == null || playerAnimator == null)
        {
            //... and the scene is not the menu or the splashscreen.
            if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "SplashScreen")
            {
                //... the player variables are set.
                playerAA = Camera.main.GetComponent<Antialiasing>();
                playerAnimator = Camera.main.GetComponentInParent<Animator>();

                indexOfHeadbobLayer = playerAnimator.GetLayerIndex("Headbob Layer");
                currentHeadbobAmount = playerAnimator.GetLayerWeight(indexOfHeadbobLayer) * 100;
            }
        }
        else
        {
            // The FOV is set
            if (Camera.main.fieldOfView != fovSetting.CurrentFov)
            {
                Camera.main.fieldOfView = fovSetting.CurrentFov;
            }

            // The amount of headbob is set
            if (currentHeadbobAmount != headbobSetting.CurrentValue)
            {
                float setValue = headbobSetting.CurrentValue == 0 ? (0.01f) : (headbobSetting.CurrentValue / 100);

                playerAnimator.SetLayerWeight(indexOfHeadbobLayer, setValue);
                currentHeadbobAmount = playerAnimator.GetLayerWeight(indexOfHeadbobLayer) * 100;
            }

            // The AA type is set
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
