using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
   // public GameObject mainScreen;
    public GameObject createRoomScreen;
    public GameObject lobbyRoomScreen;
    public GameObject lobbyBrowserScreen;

    public Button createRoomButton;
    public Button findRoomButton;

    public TMP_Text playerListText;
    public TMP_Text roomInfoText;

    public Button startGameButton;

    //lobbyBrowser
    public RectTransform roomListContainer;
    public GameObject roomButtonPrefab;

    private List<GameObject> roomButtons = new List<GameObject>();
    private List<RoomInfo> roomslist = new List<RoomInfo>();

    private void Start()
    {
        //disable the menu button at the start
        createRoomButton.interactable = false;
        findRoomButton.interactable = false;

        //enable the cursor since we hided it when we play the game
        Cursor.lockState = CursorLockMode.None;

        if (PhotonNetwork.InRoom)
        {
            //go to Lobby

            //make the game visible again
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
    }
    public void DisableScreens()
    {
        createRoomScreen.SetActive(false);
        lobbyRoomScreen.SetActive(false);
        lobbyBrowserScreen.SetActive(false);

    }

    public void OnPlayerNameValueChanged(TMP_InputField playerNameInput)
    {
        PhotonNetwork.NickName = playerNameInput.text;

    }
    public override void OnConnectedToMaster()
    {
        // enable the menu Button When we connect to the server
        createRoomButton.interactable = true;
        findRoomButton.interactable = true;
    }
    public void OnClick_CreateRoom(TMP_InputField roomName)
    {
        NetworkManager.instance.CreateRoom(roomName.text);
    }

    public override void OnJoinedRoom()
    {
        DisableScreens();
        lobbyRoomScreen.SetActive(true);

        photonView.RPC(nameof(UpdateLobbyUI), RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateLobbyUI();
    }

    [PunRPC]
    void UpdateLobbyUI()
    {
        //enable or disable the start button depending on if we are the Host
        startGameButton.interactable = PhotonNetwork.IsMasterClient;

        playerListText.text = "";

        foreach(Player player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n";
        }

        //Update Roominfo Text
        roomInfoText.text = "<b>Room Name: " + PhotonNetwork.CurrentRoom.Name + "</b>";
    }
    
    public void OnClick_StartGame()
    {
        //Hide & Close The Room
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;

        //Load into the game scene with all players in room
        NetworkManager.instance.photonView.RPC("LoadScene", RpcTarget.All, "Game_Scene");
    }
    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Left Room");
        DisableScreens();
    }

    GameObject CreateRoomButton()
    {
        GameObject btnObj = Instantiate(roomButtonPrefab, roomListContainer.transform);
        roomButtons.Add(btnObj);
        return btnObj;
    }

    void UpdateLobbyBrowserUI()
    {
        //disabble all Room Buttons
        foreach(GameObject button in roomButtons)
        {
            button.SetActive(false);
        }

        //display all current rooms in the master server
        for(int x = 0; x < roomslist.Count; x++)
        {
            // get or create room button
            GameObject button = x >= roomButtons.Count ? CreateRoomButton() : roomButtons[x];
            button.SetActive(true);

            //set the room name and Player count
            button.transform.Find("RoomName_Text").GetComponent<TMP_Text>().text = roomslist[x].Name;
            button.transform.Find("Player_Count").GetComponent<TMP_Text>().text = roomslist[x].PlayerCount + "/" + roomslist[x].MaxPlayers;

            //Set The button On Click Event
            Button buttonCmp = button.GetComponent<Button>();

            //save the room name
            string roomName = roomslist[x].Name;

            buttonCmp.onClick.RemoveAllListeners();
            buttonCmp.onClick.AddListener(()=> { NetworkManager.instance.JoinRoom(roomName); });
        }
    }
    public void OnClick_RefreshList()
    {
        UpdateLobbyBrowserUI();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        roomslist = roomList;
    }

}
