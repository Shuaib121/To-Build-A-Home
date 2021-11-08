using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public GameObject RemoveMe;
    void Start()
    {
        StartCoroutine(ShowAndHide(6.0f));
    }
    IEnumerator ShowAndHide(float delay)
    {
        RemoveMe.SetActive(true);
        yield return new WaitForSeconds(delay);
        RemoveMe.SetActive(false);
    }
}
