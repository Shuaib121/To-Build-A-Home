using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public CharController charController;

    public BuildingPlacer buildingPlacer;

    public Transform theDestination;

    public RangeChecker rangeChecker;

    private bool isPickedUp = false;

    private void Start()
    {
        rangeChecker = (RangeChecker)GameObject.Find("Range").GetComponent("RangeChecker");
        buildingPlacer = (BuildingPlacer)GameObject.Find("GameManager").GetComponent("BuildingPlacer");
        charController = (CharController)GameObject.Find("Character").GetComponent("CharController");
        theDestination = buildingPlacer.placementIndicator.transform;
    }

    private void Update()
    {
        if (isPickedUp)
        {
            PlacingObject();
        }

        if (Input.GetKeyDown(KeyCode.Q) && isPickedUp == false && !charController.hasObject)
        {
            if (rangeChecker.GetFirstObjectName() != this.gameObject.name)
            {
                return;
            }

            charController.hasObject = true;
            isPickedUp = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && isPickedUp == true && charController.isInRoom)
        {
            PlaceObject();
        }

        if (Input.GetKey(KeyCode.R) && isPickedUp && charController.hasObject && charController.isInRoom)
        {
            buildingPlacer.RotateObject(true);
        }
        else if (Input.GetKey(KeyCode.E) && isPickedUp && charController.hasObject && charController.isInRoom)
        {
            buildingPlacer.RotateObject(false);
        }
    }

    private void PlacingObject()
    {
        buildingPlacer.BeginNewBuildingPlacement(this.gameObject);

        this.transform.position = theDestination.position;
        this.transform.parent = theDestination.transform;
    }

    private void PlaceObject()
    {
        charController.hasObject = false;
        isPickedUp = false;
        buildingPlacer.PlaceBuilding();
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void PickupOrPlace()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isPickedUp == false && !charController.hasObject)
        {
            if (rangeChecker.GetFirstObjectName() != this.gameObject.name)
            {
                return;
            }

            charController.hasObject = true;
            isPickedUp = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && isPickedUp == true && charController.isInRoom)
        {
            PlaceObject();
        }
    }
}
