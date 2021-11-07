using System;
using System.Collections;
using System.Collections.Generic;
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
    public List<GameObject> rotateButtons;
    public AudioScript audioSource;
    public GameObject animatedCharacter;


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
        {
            Move();
        }
        else
        {
            //Set character to stop running with the bool "IsRunning"
            animatedCharacter.GetComponent<Animator>().SetBool("IsRunning", false);
        }

        if (hasObject)
            PlacingObject();

        if(hasObject && isInRoom)
        {
            ToggleRotateButtons(true);
        }
        else
        {
            ToggleRotateButtons(false);
        }
    }

    private void ToggleRotateButtons(bool toggle)
    {
        foreach(GameObject button in rotateButtons)
        {
            button.SetActive(toggle);
        }
    }

    void Move()
    {
        //Set character to start running with the bool "IsRunning"
        animatedCharacter.GetComponent<Animator>().SetBool("IsRunning", true);

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

            if(heldObject != null) audioSource.PlaySound(1);

            hasObject = true;
        }
        else if (hasObject && isInRoom)
        {
            audioSource.PlaySound(1);
            PlaceObject();
        }
    }
}


