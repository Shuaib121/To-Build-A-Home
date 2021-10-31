using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<CharController>(out CharController charController))
        {
            charController.isInRoom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<CharController>(out CharController charController))
        {
            charController.isInRoom = false;
        }
    }
}
