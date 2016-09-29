using UnityEngine;
using System.Collections;

public class HideDoor : MonoBehaviour
{
    [SerializeField]
    private IntroSequence introSequence;

    void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;

        if (player.parent != null)
        {
            if (player.parent.tag == "Player")
            {
                introSequence.firstCleared = true;
                gameObject.SetActive(false);

            }
        }
    }
}
