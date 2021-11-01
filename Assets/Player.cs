using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ToBuildAHome.IPlayerActions
{
    public Vector3 direction;

    private bool isFaceMethodRunning = false;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 readVector = context.ReadValue<Vector2>();
        Vector3 toConvert = new Vector3(readVector.x, 0, readVector.y);
        direction = IsoVectorConvert(toConvert);

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
