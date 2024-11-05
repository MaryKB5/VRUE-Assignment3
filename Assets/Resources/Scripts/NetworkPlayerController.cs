using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR;

public class NetworkPlayerController : MonoBehaviourPun
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private void Start()
    {
        
    }

    private void Update()
    {
        MapPosition(head, XRNode.Head);
        MapPosition(leftHand, XRNode.LeftHand);
        MapPosition(rightHand, XRNode.RightHand);
    }

    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;
    }
}