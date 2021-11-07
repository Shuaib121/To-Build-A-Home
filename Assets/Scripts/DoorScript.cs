using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Character")
        {
            door.GetComponent<Animator>().Play("Door_Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Character")
        {
            door.GetComponent<Animator>().Play("Door_Close");
        }
    }
}
