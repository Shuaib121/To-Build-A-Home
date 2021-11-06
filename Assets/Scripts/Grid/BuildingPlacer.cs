﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject placeableObject;
    public GameObject aboveHeadPosition;
    public CharController charController;
    public float gridSpeed = 1000;
    public float rotSpeed = 3250;
    public float damping = 75;

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
                //placementIndicator.transform.position = curPlacementPos;
                placementIndicator.transform.position = Vector3.MoveTowards(placementIndicator.transform.position, curPlacementPos, Time.deltaTime * gridSpeed);
                placementIndicator.transform.rotation = Quaternion.identity;
            }
        }
    }

    public void BeginNewBuildingPlacement(GameObject placeableObject)
    {
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
        placeableObject.transform.parent = null;
        placeableObject.transform.position = curPlacementPos;

        CancelBuildingPlacement();
    }

    public void RotateObject(bool rotateRight)
    {
        if (rotateRight)
        {
            desiredRot += rotSpeed * Time.deltaTime;
            var desiredRotQ = Quaternion.Euler(placeableObject.transform.eulerAngles.x, desiredRot, placeableObject.transform.eulerAngles.z);
            placeableObject.transform.rotation = Quaternion.Lerp(placeableObject.transform.rotation, desiredRotQ, Time.deltaTime * damping);
            //placeableObject.transform.Rotate(0, 45, 0);
        }
        else
        {
            desiredRot -= rotSpeed * Time.deltaTime;
            var desiredRotQ = Quaternion.Euler(placeableObject.transform.eulerAngles.x, desiredRot, placeableObject.transform.eulerAngles.z);
            placeableObject.transform.rotation = Quaternion.Lerp(placeableObject.transform.rotation, desiredRotQ, Time.deltaTime * damping);
            //placeableObject.transform.Rotate(0, -45, 0);
        }
    }
}