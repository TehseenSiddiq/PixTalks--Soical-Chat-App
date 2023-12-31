using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UiManager : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static UiManager Instance;

    [SerializeField]
    private GameObject loginPanel;
    [SerializeField]
    private GameObject registrationPanel;
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private GameObject groupPanel;
    [SerializeField]
    private GameObject groupInfoPanel;
    [SerializeField]
    private GameObject groupCreatePanel;

    [Header("Login Crediantial")]
    public TMP_InputField email_Field_Login;
    public TMP_InputField password_Field_Login;
    public TextPopUpEffect loginError;

    public string username;

    [Header("Registeration")]
    public TMP_InputField fullname_Field;
    public TMP_InputField username_Field;
    public TMP_InputField email_Field_Register;
    public TMP_InputField phone_Field_Register;
    public TMP_InputField password_Field_Register;
    public TMP_InputField confirmpassword_Field;

    [Header("GroupInfo")]
    public Button createRoomButton;
    public TMP_InputField roomName;
    public Slider noOfPlayers;

    public TMP_Text playerListText;
    public TMP_Text roomInfoText;
    public TMP_Text roomPeopleCountText;

    private List<GameObject> roomButtons = new List<GameObject>();
    private List<RoomInfo> roomslist = new List<RoomInfo>();

    //GroupBrowser
    public RectTransform groupListContainer;
    public GameObject groupButtonPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        //disable the menu button at the start
        createRoomButton.interactable = false;

        if (PhotonNetwork.InRoom)
        {
            //go to Lobby

            //make the game visible again
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
    }

    public override void OnConnectedToMaster()
    {
        // enable the menu Button When we connect to the server
        createRoomButton.interactable = true;
    }

    public void OnClick_CreateRoom()
    {
        NetworkManager.instance.CreateRoom(roomName.text,noOfPlayers.value);
    }
    public override void OnJoinedRoom()
    {
        groupPanel.SetActive(true);

        photonView.RPC(nameof(UpdateLobbyUI), RpcTarget.All);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateLobbyUI();
    }
    [PunRPC]
    void UpdateLobbyUI()
    {        
        playerListText.text = "";

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n";
        }

        //Update Roominfo Text
        roomInfoText.text = PhotonNetwork.CurrentRoom.Name;
        roomPeopleCountText.text = "Group. " + PhotonNetwork.PlayerList.Length + " People";
        
    }
    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Left Group");
    
    }
    GameObject CreateGroupButton()
    {
        GameObject btnObj = Instantiate(groupButtonPrefab, groupListContainer.transform);
        roomButtons.Add(btnObj);
        return btnObj;
    }
    void UpdateGroupBrowserUI()
    {
        //disabble all Room Buttons
        foreach (GameObject button in roomButtons)
        {
            button.SetActive(false);
        }

        //display all current rooms in the master server
        for (int x = 0; x < roomslist.Count; x++)
        {
            // get or create room button
            GameObject button = x >= roomButtons.Count ? CreateGroupButton() : roomButtons[x];
            button.SetActive(true);

            //set the room name and Player count
            button.transform.Find("GroupName_Text").GetComponent<TMP_Text>().text = roomslist[x].Name;
           // button.transform.Find("Player_Count").GetComponent<TMP_Text>().text = roomslist[x].PlayerCount + "/" + roomslist[x].MaxPlayers;

            //Set The button On Click Event
            Button buttonCmp = button.GetComponent<Button>();

            //save the room name
            string roomName = roomslist[x].Name;

            buttonCmp.onClick.RemoveAllListeners();
            buttonCmp.onClick.AddListener(() => { NetworkManager.instance.JoinRoom(roomName); });
        }
    }
    public void OnClick_RefreshList()
    {
        UpdateGroupBrowserUI();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        roomslist = roomList;
    }

    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        if (registrationPanel.activeSelf)
            registrationPanel.GetComponent<FadeInOut>().Hide();
        if (mainPanel.activeSelf)
            mainPanel.GetComponent<FadeInOut>().Hide();
        if (groupPanel.activeSelf)
            groupPanel.GetComponent<FadeInOut>().Hide();
        if (groupInfoPanel.activeSelf)
            groupInfoPanel.GetComponent<FadeInOut>().Hide();
        if(groupCreatePanel.activeSelf)
            groupCreatePanel.GetComponent<FadeInOut>().Hide();

    }
    public void OpenRegistrationPanel()
    {
        registrationPanel.SetActive(true);
        if(loginPanel.activeSelf)
            loginPanel.GetComponent<FadeInOut>().Hide();
        if (mainPanel.activeSelf)
            mainPanel.GetComponent<FadeInOut>().Hide();
        if (groupPanel.activeSelf)
            groupPanel.GetComponent<FadeInOut>().Hide();
        if (groupInfoPanel.activeSelf)
            groupInfoPanel.GetComponent<FadeInOut>().Hide();
        if (groupCreatePanel.activeSelf)
            groupCreatePanel.GetComponent<FadeInOut>().Hide();
    }
    public void OpenMainPanel()
    {
        mainPanel.SetActive(true);
        if (loginPanel.activeSelf)
            loginPanel.GetComponent<FadeInOut>().Hide();
        if (registrationPanel.activeSelf)
            registrationPanel.GetComponent<FadeInOut>().Hide();
        if (groupPanel.activeSelf)
            groupPanel.GetComponent<FadeInOut>().Hide();
        if (groupInfoPanel.activeSelf)
            groupInfoPanel.GetComponent<FadeInOut>().Hide();
        if (groupCreatePanel.activeSelf)
            groupCreatePanel.GetComponent<FadeInOut>().Hide();
    }
    public void OpenGroupPanel()
    {
        groupPanel.SetActive(true);
        if (loginPanel.activeSelf)
            loginPanel.GetComponent<FadeInOut>().Hide();
        if (registrationPanel.activeSelf)
            registrationPanel.GetComponent<FadeInOut>().Hide();
        if (mainPanel.activeSelf)
            mainPanel.GetComponent<FadeInOut>().Hide();
        if (groupInfoPanel.activeSelf)
            groupInfoPanel.GetComponent<FadeInOut>().Hide();
        if (groupCreatePanel.activeSelf)
            groupCreatePanel.GetComponent<FadeInOut>().Hide();
    }

    public void RegisterUser()
    {
        if(password_Field_Register.text != confirmpassword_Field.text)
        {
            Debug.Log("Password Doesnot Match.");
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Username = fullname_Field.text,
            DisplayName = username_Field.text,
            Email = email_Field_Register.text,
            Password = password_Field_Register.text,

            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnregisterSucces, OnError);
    }
    public void LoginUser()
    {
        if (password_Field_Register.text != confirmpassword_Field.text)
        {
            Debug.Log("Password Doesnot Match.");
        }
        var request = new LoginWithEmailAddressRequest
        {
            Email = email_Field_Login.text,
            Password = password_Field_Login.text,

            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
            

        };
        NetworkManager.instance.ConnectToServer();
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSucces, OnLoginError);
    }

    private void OnLoginSucces(LoginResult obj)
    {
        OpenMainPanel();
        if(obj.InfoResultPayload != null)
            username = obj.InfoResultPayload.PlayerProfile.DisplayName;            
        Debug.Log("Login Success.");
        UpdateGroupBrowserUI();
    }
    private void OnLoginError(PlayFabError Error)
    {
        loginError.Play();
        Debug.Log("Error Logining Account");
    }
   
    private void RecoverUser()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = email_Field_Register.text,
            TitleId = "4276C",
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySucces, OnError);
    }

    private void OnRecoverySucces(SendAccountRecoveryEmailResult obj)
    {
        throw new NotImplementedException();
    }

    private void OnregisterSucces(RegisterPlayFabUserResult Result)
    {
        Debug.Log("Newly Created Account");
        OpenLoginPanel();
    }

    private void OnError(PlayFabError Error)
    {
        Debug.Log("Error Creating Account "+Error);
    }
}
