using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public TMPro.TMP_Text message_text;
    public bool isDeleted = false;
    public bool deletionConfirmed = false;
    public string senderUsername;

  
    public void DeleteMessage()
    {
        if (senderUsername == UiManager.Instance.username)
        { 
                // Show confirmation prompt
                // Set deletionConfirmed to true upon confirmation
                PanelPopUP confirmPanel = FindObjectOfType<PanelPopUP>();
                confirmPanel.POP();
            confirmPanel.transform.Find("ConfirmBtn").GetComponent<Button>().onClick.AddListener(()=> 
            {
                // Send request to server to delete message on all clients
                //   GetComponent<PhotonView>().RPC("DeleteMessageOnServer", RpcTarget.MasterClient, gameObject.name);
                DeleteMessageOnServer(gameObject.name);
                // Update local text to "This message was deleted"
                
                isDeleted = true;
            }); 
        }
     
    }
    void DeleteConfirmed()
    {
        
    }

    void DeleteMessageOnServer(string messageName)
    {
        Debug.Log(messageName);
        // Validate ownership on server using senderUsername from message data
        if (PhotonNetwork.NickName == senderUsername)
        {
            // Remove message object from server storage (modify based on your implementation)
            // ... (e.g., database update or object deletion)
            // Broadcast removal notification to all clients (except sender)
      
            message_text.text = "<b><color=red>" + PhotonNetwork.NickName + "</color></b>\n<color=#9F9F9F>This message was deleted.</color>";
            Debug.Log("Condition#2"+messageName);
            GetComponent<PhotonView>().RPC("RemoveMessage", RpcTarget.Others, messageName);
        }
        else
        {
            // Show message indicating cannot delete other users' messages (optional)
        }
    }
    [PunRPC]
    public void RemoveMessage(string messageName)
    {
        // Find the message object with the matching name in the chat content
        Debug.Log(messageName);
        Message messageToDelete = FindObjectOfType<Chat>().content.Find(messageName).GetComponent<Message>();

        // Check if found to avoid potential errors
        if (messageToDelete)
        {
            Debug.Log("Condition#3" +messageName);
            // Destroy the message object from the scene, removing it from the UI
            messageToDelete.message_text.text = "<b><color=red>" + UiManager.Instance.username + "</color></b>\n<color=#9F9F9F>This message was deleted.</color>";
         //   Destroy(messageToDelete.gameObject);
        }
    }
}
