using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Chat : MonoBehaviour
{
    public TMP_InputField inputField;
    public string message;
    public Transform content;
    public PhotonView photonView;

    public void SendMessage()
    {
       ;
       photonView.RPC("GetMessage", RpcTarget.All, inputField.text, GenerateRandomString(5), PhotonNetwork.NickName);
    }
    [PunRPC]
    public void GetMessage(string ReceiveMessage,string ID,string senderName)
    {
        Debug.Log("Name: "+PhotonNetwork.NickName);
        GameObject m = PhotonNetwork.Instantiate(message, content.position,Quaternion.identity);
        m.transform.parent = content;
        m.transform.localScale = new Vector3(1,1,1);
        m.GetComponent<Message>().message_text.text = "<b><color=red>"+ senderName+"</color></b> \n"+ReceiveMessage;
        m.GetComponent<Message>().senderUsername = UiManager.Instance.username;
        m.name = ID;
        inputField.text = "";
    }
    
    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        
        //Random random = new Random();
        string ID = new string("");
        for (int i = 0; i < length; i++)
        {
            ID += chars[Random.Range(0, length)];
        }
        return ID;
    }

}
