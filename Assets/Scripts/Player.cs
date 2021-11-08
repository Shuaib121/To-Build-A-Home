using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ToBuildAHome.IPlayerActions
{
    [SerializeField] float moveSpeed = 4f;
    public Vector3 direction;
    public bool isDragging = false;

    private bool isFaceMethodRunning = false;
    private Vector3 forward, right;

    void Start()
    {

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        // -45 degrees from the world x axis
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //Move();

        Vector2 readVector = context.ReadValue<Vector2>();
        Vector3 toConvert = new Vector3(readVector.x, 0, readVector.y);
        direction = IsoVectorConvert(toConvert);

        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        if (!isFaceMethodRunning)
        {
            StartCoroutine(FaceDirection(direction));
        }
    }

    public Vector3 IsoVectorConvert(Vector3 vector)
    {
        Quaternion rotation = Quaternion.Euler(0, 45.0f, 0);
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(rotation);
        Vector3 result = isoMatrix.MultiplyPoint3x4(vector);
        return result;
    }

    void Move()
    {

        // Movement speed
        Vector3 rightMovement = right * moveSpeed * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Input.GetAxis("Vertical");

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

    IEnumerator FaceDirection(Vector3 heading)
    {
        isFaceMethodRunning = true;

        yield return new WaitForSeconds(0.05f);

        isFaceMethodRunning = false;

        if (Input.GetAxis("VerticalKey") != 0 || Input.GetAxis("HorizontalKey") != 0 || isDragging)
        {
            Debug.Log("TEST");
            transform.forward = heading;
        }
    }
}
