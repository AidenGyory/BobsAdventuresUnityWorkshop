using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    Transform player;

    public float distance = 5;
    float defaultDistance;
    float currentDistance = 5;
    private Vector2 currentRotation = new Vector2();
    public Vector2 rotateSpeed = new Vector2(50, -50);
    Vector3 lookPosition;
    public Vector3 offset;

    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        defaultDistance = distance;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.instance.Locked())
        {
            lookPosition = player.position;
        }
        if (Input.GetMouseButton(0))
        {
            currentRotation.x += Input.GetAxis("Mouse X") * rotateSpeed.x * Time.deltaTime * 10;
            currentRotation.y += Input.GetAxis("Mouse Y") * rotateSpeed.y * Time.deltaTime * 10;
        }

        RaycastHit hit;
        bool raycast = Physics.Raycast(player.position + offset, -transform.forward, out hit, distance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);
        if (raycast)
        {
            currentDistance = hit.collider.gameObject.tag != "IgnoreCamera" && hit.collider.gameObject.tag != "Player" ? hit.distance : Mathf.MoveTowards(currentDistance, distance, Time.deltaTime * 7);
        }
        else
        {
            currentDistance = Mathf.MoveTowards(currentDistance, distance, Time.deltaTime * 7);
        }

        Vector3 direction = new Vector3(0, 0, -currentDistance);

        Quaternion rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

        transform.position = (lookPosition + offset) + rotation * direction;

        transform.LookAt(lookPosition + offset);
    }

    public void Lock(Vector3 position, float cameraDistance)
    {
        lookPosition = position;
        distance = cameraDistance;
    }

    public void Unlock()
    {
        distance = defaultDistance;
    }
}
