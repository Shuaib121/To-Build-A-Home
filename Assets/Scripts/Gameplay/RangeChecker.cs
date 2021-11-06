using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeChecker : MonoBehaviour
{
    [SerializeField]
    private List<Collider> colliders = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent("PickUp")) return;

        if (!colliders.Contains(other)) colliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }

    public string GetFirstObjectName()
    {
        if(colliders.Count > 0)
        {
            return colliders[0].gameObject.name;
        }

        return "empty";
    }

    public GameObject GetFirstObject()
    {
        return colliders[0].gameObject;
    }
}
