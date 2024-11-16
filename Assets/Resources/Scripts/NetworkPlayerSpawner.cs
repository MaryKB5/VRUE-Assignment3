using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    public override void OnJoinedRoom()
    {
        Debug.Log("NetworkPlayerSpawner OnJoinedRoom");
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
        spawnedPlayerPrefab.transform.name = "Player " + PhotonNetwork.LocalPlayer.ActorNumber;
        spawnedPlayerPrefab.transform.SetParent(Camera.main.transform);
    }
    public override void OnLeftRoom()
    {
        Debug.Log("NetworkPlayerSpawner OnLeftRoom");
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
