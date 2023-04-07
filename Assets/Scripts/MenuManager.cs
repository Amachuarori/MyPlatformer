using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField nameForCreate;
    [SerializeField] InputField nameForJoin;
    
    public void CreateRoom(){
        RoomOptions room = new RoomOptions();
        room.IsOpen = true;
        room.IsVisible = true;
        room.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(nameForCreate.text, room);
    }

    public void JoinRoom(){
        PhotonNetwork.JoinRoom(nameForJoin.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
