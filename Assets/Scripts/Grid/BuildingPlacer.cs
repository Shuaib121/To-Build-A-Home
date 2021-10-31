using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject placeableObject;
    public GameObject aboveHeadPosition;
    public CharController charController;

    private bool currentlyPlacing;

    private float placementIndicatorUpdateRate = 0.05f;
    private float lastUpdateTime;
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
                placementIndicator.transform.rotation = Quaternion.identity;
            }
        }
    }

    public void BeginNewBuildingPlacement(GameObject placeableObject)
    {
        this.placeableObject = placeableObject;
        currentlyPlacing = true;
    }

    public void CancelBuildingPlacement()
    {
        currentlyPlacing = false;
    }

    public void PlaceBuilding()
    {
        placeableObject.transform.parent = null;
        placeableObject.transform.position = curPlacementPos;

        CancelBuildingPlacement();
    }

    public void RotateObject(bool rotateRight)
    {
        if (rotateRight)
        {
            placeableObject.transform.Rotate(0, 45, 0);
        }
        else
        {
            placeableObject.transform.Rotate(0, -45, 0);
        }
    }
}