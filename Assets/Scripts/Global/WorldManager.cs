using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private bool destroyOnLoad = true;

    private void Awake()
    {
        if (!destroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
