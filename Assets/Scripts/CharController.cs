using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f; //Change in inspector to adjust move speed

    Vector3 forward, right; // Keeps track of our relative forwards and right vectors

    void Start()
    {
        forward = Camera.main.transform.forward; //set forward to equal the camera's forward vector
        forward.y = 0; //make sure y is 0
        forward = Vector3.Normalize(forward); //make sure the length of vector is set to a max of 1.0
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; //set the right-facing vector to be facing right relative to the camera's forward vector

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            Move();
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0f, Input.GetAxis("VerticalKey"));
            // setup a direction vector based on keyboard input.GetAxis returns a value between - 1.0 and 1.0.
            //If the A key is pressed, GetAxis(HorizontalKey) will return -1.0.If D is pressed, it will return 1.0
        
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey"); //Our right movement is 
            // based on the right vector, movement speed, and our GetAxiis command. we multiply by Time.deltaTime to make the movement smooth
        
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey"); 
            //Up movement uses the forward vector, movement speed, and the vertical axis inputs
        
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        //This creates our new direction. By combining our right and forward movements and normalizing them, 
        //we create a new vector that points in the appropriate direction with a length no greater than 1.0

        transform.forward = heading; //sets forward direction of game object to whatever direction we're moving in
        transform.position += rightMovement; //move transform's position right/left
        transform.position += upMovement; //move transform's position up/down
    }

}


