using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpin : MonoBehaviour
{
    [SerializeField] private float upDownSpeed = 0.4f;
    [SerializeField] private float rotationSpeed = 360f;

    private Transform player;
    private float defaultY;
    private float rotationOffset;

    private void Start()
    {
        defaultY = transform.position.y;
        player = GameObject.FindWithTag("Player").transform;

        // Generate a random rotation offset for each instance of the coin
        rotationOffset = Random.Range(0f, 360f);
    }

    private void Update()
    {
        Vector3 lookPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookPosition);

        float rotationAngle = rotationOffset + (Time.time * rotationSpeed);
        Quaternion rotation = Quaternion.Euler(0f, rotationAngle, 0f);
        transform.rotation = rotation;

        transform.position = new Vector3(transform.position.x, defaultY + (Mathf.Sin(Time.time * upDownSpeed) * 0.4f), transform.position.z);
    }
}
