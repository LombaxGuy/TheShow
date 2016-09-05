using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
