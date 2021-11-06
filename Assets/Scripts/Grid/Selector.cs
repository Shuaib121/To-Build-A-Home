using UnityEngine;

public class Selector : MonoBehaviour
{
    private Camera cam;
    private Vector3 oldPos = Vector3.zero;

    public static Selector inst;
    public GameObject placementObj;
    public BuildingPlacer buildingPlacer;

    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        cam = Camera.main;
        buildingPlacer = (BuildingPlacer)GameObject.Find("GameManager").GetComponent("BuildingPlacer");
    }

    public Vector3 GetCurTilePosition()
    {
        Vector3 fwd = placementObj.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(placementObj.transform.position, fwd, out hit, 10))
            print("There is something in front of the object!");

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(hit.point));

        float rayOut = 0.0f;

        if(plane.Raycast(cam.ScreenPointToRay(cam.WorldToScreenPoint(hit.point)), out rayOut))
        {
            Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(0.5f, 0.0f, 0.5f);
            var pos = new Vector3(Mathf.CeilToInt(newPos.x), 0, Mathf.CeilToInt(newPos.z));

            if (buildingPlacer.placedObjects.Contains(pos))
            {
                return oldPos;
            }
            else
            {
                oldPos = pos;
                return pos;
            }
            //return new Vector3(Mathf.CeilToInt(newPos.x), 0, Mathf.CeilToInt(newPos.z));
        }

        return new Vector3(0, -99, 0);
    }
}