using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject prefab;
    public GameObject indicatorPrefab;
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
        theDestination = GameObject.Find("HideObjects").transform;


        if (!indicatorPrefab)
        {
            var gameObjectName = this.gameObject.name.Split('_');
            indicatorPrefab = buildingPlacer.placementIndicator.transform.Find($"{gameObjectName[0]} Variant").gameObject;
        }
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
            indicatorPrefab.transform.rotation = this.gameObject.transform.rotation;
            indicatorPrefab.SetActive(true);
            isPickedUp = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && isPickedUp == true && charController.isInRoom)
        {
            PlaceObject();
            indicatorPrefab.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R) && isPickedUp && charController.hasObject && charController.isInRoom)
        {
            buildingPlacer.RotateObject(true);
            indicatorPrefab.transform.rotation = this.gameObject.transform.rotation;
        }
        else if (Input.GetKeyDown(KeyCode.E) && isPickedUp && charController.hasObject && charController.isInRoom)
        {
            buildingPlacer.RotateObject(false);
            indicatorPrefab.transform.rotation = this.gameObject.transform.rotation;
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
}
