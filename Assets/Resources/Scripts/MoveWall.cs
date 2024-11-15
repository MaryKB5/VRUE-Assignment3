using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public float moveDistance = 2.0f;
    public float moveSpeed = 2.0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition + Vector3.right * Mathf.Sin(Time.time * moveSpeed) * moveDistance;
    }
}
