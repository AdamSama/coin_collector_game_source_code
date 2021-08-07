// using System.Collections;
// using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CameraController : MonoBehaviour
{
    public float speed = 7.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;

    // public AudioSource audio;
    // public AudioClip walkAudio;
    public static CameraController cc;

    public CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;

    void Awake()
    {
        cc = this;
    }

    void Start()
    {
        // audio = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    void Update()
    {
        if(GameManager.manager.paused || GameManager.manager.win || PlayerController.player.dead || GameManager.manager.timer<0)
            return;

        if (transform.position.y <= -3.0f)
        {
            Debug.Log("You are out");
            OutOfBounds();
        }

        if (characterController.isGrounded)
        {
            // Debug.Log("Grounded");
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }
            // if (characterController.velocity.magnitude > 0)
            // {
            //     PlayerController.player.MakeSound(walkAudio);
            // }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
    }

    void OutOfBounds()
    {
        PlayerController.player.dead = true;
        GameManager.manager.Out();
    }
}

