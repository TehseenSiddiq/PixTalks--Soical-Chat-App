
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ConnectToServer()
    {
        //Connect to master
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Start function");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        
       PhotonNetwork.NickName = UiManager.Instance.username;

        Debug.Log("Joined The Master Server");
    }
    public override void OnJoinedLobby()
    {
        //CreateRoom("TestRoom");
    }
    public void CreateRoom(string roomName,float maxPlayers)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)maxPlayers;

        PhotonNetwork.CreateRoom(roomName, options);
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("Joining Room");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Sccuessfully Joined the :" + PhotonNetwork.CurrentRoom.Name);
        UiManager.Instance.OpenGroupPanel();
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed " + message);
    }
    [PunRPC]
    public void LoadScene(string SceneName)
    {
        PhotonNetwork.LoadLevel(SceneName);
    }
}
