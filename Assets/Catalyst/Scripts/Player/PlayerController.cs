using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(CharacterController))] //auto add a character controller if the gameobject doesn't have one
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Animator animator; //set in inspector
    public Transform cam;
    CharacterController cc; //set in start function (through code)

    public CharacterController Controller => cc;

    [Header("Movement Parameters")]
    public float speed = 5;
    public float gravity = 10;
    public float jumpWeight = 10;
    public float jumpHeight = 5; //all set by user in the unity inspector

    private bool jumping = false; //true when player is jumping
    private Vector3 movement = new Vector3(); //not set by user
    private Vector3 jumpBoost = Vector3.zero; // Stores horizontal jump boost
    bool locked = false;
    public static PlayerController instance;
    PlayerInput playerInput;

    public Platform attachedPlatform;

    private void Awake()
    {
        instance = this;
    }

    public void SetMeshVisibility(bool visible)
    {
        SkinnedMeshRenderer meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.shadowCastingMode = visible
                ? UnityEngine.Rendering.ShadowCastingMode.On
                : UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>(); //get reference to character controller component
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Exit play mode when pressing escape, also quit application in build
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EditorApplication.isPlaying = false;
            Application.Quit();
        }

        bool running = false;

        if (!jumping)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !locked)
            {
                Jump(jumpHeight);
            }
            else if (cc.isGrounded)
            {
                movement.y = 0;
                jumpBoost = Vector3.zero; // Reset boost on ground
            }
            else
            {
                movement.y -= (jumpWeight * Time.deltaTime);
            }
            running = Input.GetKey(KeyCode.LeftShift);
        }
        else
        {
            if (cc.isGrounded)
            {
                animator.SetTrigger("Landed");
                jumping = false;
                jumpBoost = Vector3.zero; // Reset boost on landing
            }
            else
            {
                movement.y -= (jumpWeight * Time.deltaTime);
            }
        }

        animator.SetBool("Grounded", cc.isGrounded);
        animator.speed = 1;

        if (locked)
        {
            return;
        }

        float speed = running ? 2 : 1;

        Vector3 moveInput = playerInput != null ? playerInput.GetMovementInput() : new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rotatedMove = Quaternion.Euler(0, cam.rotation.eulerAngles.y, 0) * moveInput;
        movement.x = rotatedMove.x * speed;
        movement.z = rotatedMove.z * speed;
        animator.speed = speed;

        DoAnimation();

        Vector3 finalMovement = movement + jumpBoost; // Add jump boost to movement

        if (finalMovement != Vector3.zero)
        {
            cc.Move(finalMovement * this.speed * Time.deltaTime); //move, collisions handled in character controller
        }
    }

    public void Jump(float jumpHeight)
    {
        animator.SetTrigger("Jump");
        jumping = true;
        movement.y = jumpHeight;

        // Apply a horizontal boost to jump
        Vector3 horizontalVelocity = new Vector3(movement.x, 0, movement.z);

        if (horizontalVelocity.magnitude > 0)
        {
            float boostMultiplier = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f; // Sprinting gives a stronger horizontal boost
            jumpBoost = horizontalVelocity.normalized * boostMultiplier;

        }
    }

    void DoAnimation()
    {
        bool isWalking = movement.x != 0 || movement.z != 0;
        animator.SetInteger("Speed", isWalking ? 1 : 0);
        if (isWalking)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.z), Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5);
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

    public void ChangePosition(Vector3 newPosition)
    {
        StopAllCoroutines();
        StartCoroutine(LockForOneFrame(newPosition));
    }

    IEnumerator LockForOneFrame(Vector3 newPosition)
    {
        attachedPlatform = null;

        locked = true;
        cc.enabled = false;
        yield return new WaitForEndOfFrame();

        transform.position = newPosition;

        yield return new WaitForEndOfFrame();
        locked = false;
        cc.enabled = true;
    }
}
