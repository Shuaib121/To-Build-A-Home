using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private Color color;
    private CharController charController;

    private void Start()
    {
        color = this.GetComponent<MeshRenderer>().material.color;
        charController = GameObject.Find("Character").GetComponent<CharController>();
    }

    private void Update()
    {
        if (charController.isInRoom)
        {
            SetTransparent(true);
        }
        else
        {
            SetTransparent(false);
        }
    }

    private void SetTransparent (bool setTransparent)
    {
        if (setTransparent)
        {
            this.GetComponent<MeshRenderer>().material.color = new Color(color.r, color.g, color.b, 0.2f);
        }
        else
        {
            this.GetComponent<MeshRenderer>().material.color = color;
        }
    }
}
