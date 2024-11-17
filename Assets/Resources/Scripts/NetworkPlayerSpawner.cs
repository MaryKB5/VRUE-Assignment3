using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    public override void OnJoinedRoom()
    {
        Debug.Log("NetworkPlayerSpawner OnJoinedRoom");
        base.OnJoinedRoom();

        
        if (photonView.IsMine) {
            return;
        } 

        Player[] currentPlayers = PhotonNetwork.CurrentRoom.Players.Values.ToArray();

        int index = 0;
        foreach (Player indexPlayer in currentPlayers) {
            if (indexPlayer.Equals(PhotonNetwork.LocalPlayer)) {
                break;
            }
            index++;
        }

        //Player player = 

        //int playerID = player.ActorNumber; // PhotonNetwork.LocalPlayer.ActorNumber;

        Vector3 basePosition = new Vector3(79, 18, -30);

        //Vector3 spawnOffset = new Vector3(0, 0, playerID * 10);
        Vector3 spawnOffset = new Vector3(0, 0, index * 10);

        Vector3 trackPosition = basePosition + spawnOffset;

        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Prefabs/Network Player", transform.position, transform.rotation);
        
        PhotonView playerPhotonView = spawnedPlayerPrefab.GetPhotonView();

        int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        playerPhotonView.OwnerActorNr = actorNumber;
        playerPhotonView.ControllerActorNr = actorNumber;

        spawnedPlayerPrefab.transform.name = "Player " + actorNumber;
    }
    public override void OnLeftRoom()
    {
        Debug.Log("NetworkPlayerSpawner OnLeftRoom");
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
