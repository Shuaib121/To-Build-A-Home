using System.Collections;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    public bool isInRoom = false;
    public bool hasObject = false;
    public GameObject heldObject;
    public Joystick joystick;
    public BuildingPlacer buildingPlacer;
    public Transform theDestination;
    public RangeChecker rangeChecker;


    private Vector3 forward, right;

    void Start()
    {
        rangeChecker = (RangeChecker)GameObject.Find("Range").GetComponent("RangeChecker");
        buildingPlacer = (BuildingPlacer)GameObject.Find("GameManager").GetComponent("BuildingPlacer");
        theDestination = buildingPlacer.placementIndicator.transform;

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        // -45 degrees from the world x axis
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void FixedUpdate()
    {
        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
            Move();

        if (hasObject)
            PlacingObject();
    }

    void Move()
    {

        // Movement speed
        Vector3 rightMovement = right * moveSpeed * joystick.Horizontal;
        Vector3 upMovement = forward * moveSpeed * joystick.Vertical;

        // Calculate what is forward
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        // Set new position
        Vector3 newPosition = transform.position;
        newPosition += rightMovement;
        newPosition += upMovement;

        // Smoothly move the new position
        transform.forward = heading;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
    }

    private void PlacingObject()
    {
        buildingPlacer.BeginNewBuildingPlacement(heldObject);

        heldObject.transform.position = theDestination.position;
        heldObject.transform.parent = theDestination.transform;
    }

    private void PlaceObject()
    {
        buildingPlacer.PlaceBuilding();
    }

    public void PickupOrPlace()
    {
        if (!hasObject)
        {
            heldObject = rangeChecker.GetFirstObject();
            heldObject.GetComponent<BoxCollider>().isTrigger = true;
            hasObject = true;
        }
        else if (hasObject && isInRoom)
        {
            PlaceObject();
        }
    }
}


