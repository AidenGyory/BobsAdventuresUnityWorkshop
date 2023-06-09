using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCoin : MonoBehaviour
{
    Transform player;
    float defaultY;

    private void Start()
    {
        defaultY = transform.position.y;
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 lookPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookPosition);

        transform.position = new Vector3(transform.position.x, defaultY + (Mathf.Sin(Time.time) * 0.4f), transform.position.z);
    }
}
