using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Player player;
    public float speed = 4f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.917f, transform.position.z);
        characterController.Move(player.direction * speed * Time.deltaTime);
    }
}
