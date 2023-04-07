using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject SpawnPointPlayer1;
    [SerializeField] GameObject SpawnPointPlayer2;

    void Start()
    {
        int playerNumber = PhotonNetwork.CurrentRoom.PlayerCount;
        Vector2 spawnPosition = new Vector2();
        if(playerNumber == 1){
            spawnPosition = SpawnPointPlayer1.transform.position;
        }
        else{
            spawnPosition = SpawnPointPlayer2.transform.position;
        }
        PhotonNetwork.Instantiate(player.name, spawnPosition, Quaternion.identity);
    }

}
