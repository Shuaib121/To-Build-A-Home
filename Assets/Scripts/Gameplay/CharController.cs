using System.Collections;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    public bool isInRoom = false;
    public bool hasObject = false;

    Vector3 forward, right;
    bool isFaceMethodRunning = false;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

    }

    void Update()
    {
        if (Input.GetAxis("VerticalKey") != 0 || Input.GetAxis("HorizontalKey") != 0)
            Move();
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0f, Input.GetAxis("VerticalKey"));

        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");

        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        if (!isFaceMethodRunning)
        {
            StartCoroutine(FaceDirection(heading));
        }

        transform.position += rightMovement;
        transform.position += upMovement;
    }

    IEnumerator FaceDirection(Vector3 heading)
    {
        isFaceMethodRunning = true;

        yield return new WaitForSeconds(0.05f);

        isFaceMethodRunning = false;

        if (Input.GetAxis("VerticalKey") != 0 || Input.GetAxis("HorizontalKey") != 0)
        {
            transform.forward = heading;
        }
    }
}


