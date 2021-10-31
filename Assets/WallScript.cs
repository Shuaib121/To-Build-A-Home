using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Character")
        {
            Debug.Log("DADAaSD");
            this.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0f, 0.5f);
        }
    }
}
