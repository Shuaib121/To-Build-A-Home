using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    private Camera cam;
    public static Selector inst;
    public GameObject test;

    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        cam = Camera.main;
    }

    public Vector3 GetCurTilePosition()
    {
        /*if(EventSystem.current.IsPointerOverGameObject())
            return new Vector3(0, -99, 0);*/

        Vector3 fwd = test.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(test.transform.position, fwd, out hit, 10))
            print("There is something in front of the object!");

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(hit.point));

        float rayOut = 0.0f;

        if(plane.Raycast(cam.ScreenPointToRay(cam.WorldToScreenPoint(hit.point)), out rayOut))
        {
            Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(0.5f, 0.0f, 0.5f);
            return new Vector3(Mathf.CeilToInt(newPos.x), 0, Mathf.CeilToInt(newPos.z));
        }

        return new Vector3(0, -99, 0);
    }
}