using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    public int maxPlayers = 10;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {

        //Connect to master
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Start function");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        
        if(PhotonNetwork.NickName == "")
        {
            string number = Random.Range(0, 9999).ToString("D4");
            string nickName = "Guest" + number;
            Debug.Log(nickName);
            PhotonNetwork.NickName = nickName;
        }
        Debug.Log("Joined The Master Server");
    }
    public override void OnJoinedLobby()
    {
        //CreateRoom("TestRoom");
    }
    public void CreateRoom(string roomName)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)maxPlayers;
        
        PhotonNetwork.CreateRoom(roomName, options);
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Sccuessfully Joined the :" + PhotonNetwork.CurrentRoom.Name);
    }
    [PunRPC]
    public void LoadScene(string SceneName)
    {
        PhotonNetwork.LoadLevel(SceneName);
    }

}
