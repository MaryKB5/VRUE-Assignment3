using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject trackPrefab;
    
    void Start()
    {
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        Debug.Log("NetworkManager ConnectToServer");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("NetworkManager OnConnectedToMaster");

        base.OnConnectedToMaster();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
    
        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("NetworkManager Joined a room");
        base.OnJoinedRoom();

        foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            SpawnTrackForPlayer(player);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);

        
        SpawnTrackForPlayer(newPlayer);        
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        var track = GameObject.Find("Track for Player " + otherPlayer.ActorNumber);
        Destroy(track);        
    }

    private void SpawnTrackForPlayer(Player player)
    { 
        Debug.Log("SpawnTrackForPlayer");
        int playerID = player.ActorNumber; // PhotonNetwork.LocalPlayer.ActorNumber;

        Vector3 basePosition = new Vector3(79, 18, -30);

        Vector3 spawnOffset = new Vector3(0, 0, playerID * 10);

        Vector3 trackPosition = basePosition + spawnOffset;
        Debug.Log("Spawning new track for playerID " + playerID + " at " + trackPosition.ToString());
        GameObject trackInstance = Instantiate(trackPrefab);
        PhotonView photonView = trackInstance.GetPhotonView();
        photonView.OwnerActorNr = player.ActorNumber;
        photonView.ControllerActorNr = player.ActorNumber;
        trackInstance.transform.name = "Track for Player " + playerID;
        trackInstance.transform.position = trackPosition;
        trackInstance.transform.rotation = Quaternion.identity; 
    }
}
