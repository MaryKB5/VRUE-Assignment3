using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Rotate : MonoBehaviourPun
{
    public float rotationSpeed = 30f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
