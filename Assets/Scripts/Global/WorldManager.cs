using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private bool destroyOnLoad = true;

    [SerializeField]
    private GameObject menuSettings;

    private void Start()
    {
    }

    private void Awake()
    {
        if (!destroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        menuSettings.SetActive(true);
    }
}
