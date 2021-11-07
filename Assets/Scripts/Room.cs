using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject roomRoof;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<CharController>(out CharController charController))
        {
            charController.isInRoom = true;
            roomRoof.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<CharController>(out CharController charController))
        {
            charController.isInRoom = false;
            roomRoof.SetActive(true);
        }
    }
}
