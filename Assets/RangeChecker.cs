using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeChecker : MonoBehaviour
{
    [SerializeField]
    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent("PickUp")) return;

        if (!colliders.Contains(other)) colliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
}
