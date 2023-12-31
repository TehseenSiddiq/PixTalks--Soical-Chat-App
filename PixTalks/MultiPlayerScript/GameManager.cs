using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Player")]
    public string PlayerPrefabPath;

    public PlayerBehavior[] players;

    public Transform[] spawnPoints;
    public float respawnTime;

    private int playerInGame;

    // Static reference to the GameManager instance.
    public static GameManager instance;

    // Add your game-related variables here.

    private void Awake()
    {
        // Check if an instance of GameManager already exists.
        if (instance == null)
        {
            // If no instance exists, set this as the instance.
            instance = this;
        }
        else if (instance != this)
        {
            // If an instance already exists and it's not this one, destroy this GameObject.
            Destroy(gameObject);
            return;
        }

       
    }
    private void Start()
    {
        // Set The length of the arry
        players = new PlayerBehavior[PhotonNetwork.PlayerList.Length];

        photonView.RPC(nameof(ImInGame), RpcTarget.AllBuffered);
    }

    [PunRPC]
    void ImInGame()
    {
        playerInGame++;
        //is the among of players in the game as same as in the room?
        if(playerInGame == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();
        }

    }
    void SpawnPlayer()
    {
        Debug.Log("Spawning Player");
        GameObject playerObj = PhotonNetwork.Instantiate(PlayerPrefabPath, spawnPoints[Random.Range(0, spawnPoints.Length)].position,Quaternion.identity);
        UIManager.instance.photonPlayer = playerObj.GetComponent<PlayerBehavior>();
        //initialize player
        playerObj.GetComponent<PhotonView>().RPC("Initialized", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
