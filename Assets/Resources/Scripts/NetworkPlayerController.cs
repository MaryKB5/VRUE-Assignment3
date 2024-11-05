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
        if (photonView.IsMine)
        {
            MapPosition(head, XRNode.Head);
            MapPosition(leftHand, XRNode.LeftHand);
            MapPosition(rightHand, XRNode.RightHand);
            
            photonView.RPC("SyncPlayerTransform", RpcTarget.Others, head.position, leftHand.position, rightHand.position, head.rotation, leftHand.rotation, rightHand.rotation);
        }
    }

    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;
    }

    [PunRPC]
    void SyncPlayerTransform(Vector3 headPosition, Vector3 leftHandPosition, Vector3 rightHandPosition, Quaternion headRotation, Quaternion leftHandRotation, Quaternion rightHandRotation)
    {
        //synch position for other players
        head.position = headPosition;
        leftHand.position = leftHandPosition;
        rightHand.position = rightHandPosition;
        head.rotation = headRotation;
        leftHand.rotation = leftHandRotation;
        rightHand.rotation = rightHandRotation;
    }
}