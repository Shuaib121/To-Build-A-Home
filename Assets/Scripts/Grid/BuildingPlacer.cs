using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject placeableObject;
    public GameObject aboveHeadPosition;
    public CharController charController;
    public List<Vector3> placedObjects;
    public float gridSpeed = 1000;
    public float rotSpeed = 3250;
    public float damping = 75;
    public bool isRotatingRight = false;
    public bool isRotatingLeft = false;

    private bool currentlyPlacing;
    private float placementIndicatorUpdateRate = 0.05f;
    private float lastUpdateTime;
    private float desiredRot;
    private Vector3 curPlacementPos;

    public GameObject placementIndicator;

    public static BuildingPlacer inst;

    void Awake()
    {
        inst = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            CancelBuildingPlacement();

        if(Time.time - lastUpdateTime > placementIndicatorUpdateRate && currentlyPlacing)
        {
            lastUpdateTime = Time.time;

            curPlacementPos = Selector.inst.GetCurTilePosition();        

            if (!charController.isInRoom)
            {
                placementIndicator.transform.parent = aboveHeadPosition.transform;
                placementIndicator.transform.position = aboveHeadPosition.transform.position;
            }
            else
            {
                placementIndicator.transform.parent = null;
                placementIndicator.transform.position = curPlacementPos;
                //placementIndicator.transform.position = Vector3.Lerp(placementIndicator.transform.position, curPlacementPos, Time.fixedDeltaTime * gridSpeed);
                placementIndicator.transform.rotation = Quaternion.identity;
            }
        }

        if (isRotatingRight && charController.isInRoom)
        {
            RotateObjectRight();
        }
        else if (isRotatingLeft && charController.isInRoom)
        {
            RotateObjectLeft();
        }
    }

    private void RotateObjectRight()
    {
        if (placeableObject == null) return;

        desiredRot += rotSpeed * Time.deltaTime;
        var desiredRotQ = Quaternion.Euler(placeableObject.transform.eulerAngles.x, desiredRot, placeableObject.transform.eulerAngles.z);
        placeableObject.transform.rotation = Quaternion.Lerp(placeableObject.transform.rotation, desiredRotQ, Time.fixedDeltaTime * damping);
    }

    private void RotateObjectLeft()
    {
        if (placeableObject == null) return;

        desiredRot -= rotSpeed * Time.deltaTime;
        var desiredRotQ = Quaternion.Euler(placeableObject.transform.eulerAngles.x, desiredRot, placeableObject.transform.eulerAngles.z);
        placeableObject.transform.rotation = Quaternion.Lerp(placeableObject.transform.rotation, desiredRotQ, Time.fixedDeltaTime * damping);
    }

    public void BeginNewBuildingPlacement(GameObject placeableObject)
    {
        if (placedObjects.Contains(placeableObject.transform.position) && !currentlyPlacing)
        {
            placedObjects.Remove(placeableObject.transform.position);
        }

        this.placeableObject = placeableObject;
        desiredRot = placeableObject.transform.eulerAngles.y;
        currentlyPlacing = true;
    }

    public void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
    }

    public void PlaceBuilding()
    {
        if (placedObjects.Contains(curPlacementPos)) return;

        placedObjects.Add(curPlacementPos);

        placeableObject.transform.parent = null;
        placeableObject.transform.position = curPlacementPos;
        placeableObject.GetComponent<BoxCollider>().isTrigger = false;
        placeableObject = null;

        charController.hasObject = false;

        CancelBuildingPlacement();
    }

    public void SetRotateRight(bool set)
    {
        isRotatingRight = set;
    }

    public void SetRotateLeft(bool set)
    {
        isRotatingLeft = set;
    }
}