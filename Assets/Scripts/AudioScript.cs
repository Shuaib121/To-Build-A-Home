using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip pickUp;
    public AudioClip menuSound;

    public void PlaySound(int num)
    {
        if (num == 1)
        {
            this.GetComponent<AudioSource>().PlayOneShot(pickUp);
        }
        else if (num == 2)
        {
            this.GetComponent<AudioSource>().PlayOneShot(menuSound);
        }
    }
}
