using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class MoveWall : MonoBehaviourPun
{
    public float moveDistance = 2.0f;
    public float moveSpeed = 2.0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    private bool collidedWithPlayer = false;

    void Update()
    {
        if (!collidedWithPlayer) {
            transform.position = startPosition + Vector3.right * Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        }
    }


    void OnCollisionEnter(Collision collision) {
        Debug.Log("MoveWall collided with " + collision.collider.name);
        collidedWithPlayer = true;
    }

    void OnCollisionExit(Collision collision) {
        collidedWithPlayer = false;
    }
}
