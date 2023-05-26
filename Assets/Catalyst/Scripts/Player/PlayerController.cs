using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] //auto add a character controller if the gameobject doesn't have one
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Animator animator; //set in inspector
    public Transform cam;
    CharacterController cc; //set in start function (through code)

    [Header("Movement Parameters")]
    public float speed = 5;
    public float gravity = 10;
    public float jumpWeight = 10;
    public float jumpHeight = 5; //all set by user in the unity inspector

    private bool jumping = false; //true when player is jumping
    private Vector3 movement = new Vector3(); //not set by user
    bool locked = false;
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>(); //get reference to character controller component
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumping)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !locked)
            {
                animator.SetTrigger("Jump");
                jumping = true;
                movement.y = jumpHeight;
            }
            else if (cc.isGrounded)
            {
                movement.y = 0;
            }
            else
            {
                movement.y -= (jumpWeight * Time.deltaTime);
            }
        }
        else
        {
            if (cc.isGrounded)
            {
                jumping = false;
            }
            else
            {
                movement.y -= (jumpWeight * Time.deltaTime);
            }
        }

        if (locked)
        {
            return;
        }

        SetMovement();

        DoAnimation();

        cc.Move(movement * speed * Time.deltaTime); //move, collisions handled in character controller
    }

    void SetMovement()
    {
        float xMove = movement.x = Input.GetAxis("Horizontal"); //left-right arrows or A-D keys
        float zMove = movement.z = Input.GetAxis("Vertical"); //up-down arrows or W-S keys
        Vector3 rotatedMove = Quaternion.Euler(0, cam.rotation.eulerAngles.y, 0) * new Vector3(xMove, 0, zMove);
        movement.x = rotatedMove.x;
        movement.z = rotatedMove.z;
    }

    void DoAnimation()
    {
        bool isWalking = movement.x != 0 || movement.z != 0;
        animator.SetInteger("Speed", isWalking ? 1 : 0);
        if (isWalking)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z), Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime*5);
        }
    }
    public void Lock()
    {
        locked = true;
        animator.SetInteger("Speed", 0);
    }

    public void Unlock()
    {
        locked = false;
    }

    public bool Locked()
    {
        return locked;
    }
}
