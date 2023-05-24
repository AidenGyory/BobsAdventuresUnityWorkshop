using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] //auto add a character controller if the gameobject doesn't have one
public class PlayerController : MonoBehaviour
{
    CharacterController cc; //set in start function (through code)

    public float speed = 5;
    public float gravity = 10;
    public float jumpWeight = 10;
    public float jumpHeight = 5; //all set by user in the unity inspector

    private bool jumping = false; //true when player is jumping
    private Vector3 movement = new Vector3(); //not set by user

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>(); //get reference to character controller component
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = -Input.GetAxis("Horizontal"); //left-right arrows or A-D keys
        movement.z = -Input.GetAxis("Vertical"); //up-down arrows or W-S keys

        if (!jumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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
                movement.y *= -0.4f;
            }
            else
            {
                movement.y -= (jumpWeight * Time.deltaTime);
            }
        }

        cc.Move(movement * speed * Time.deltaTime); //move, collisions handled in character controller
    }
}
