using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageUploader : MonoBehaviour
{
  
    public GameObject pickImageButton;
    public GameObject sendImageButton;


    public void OnPickImageButtonClicked()
    {
      /*  NativeGallery.ShowMediaSelectionPanel(result =>
        {
            if (result.filePath != null)
            {
                
                string imagePath = result.filePath;
                // ... or byte[] imageData = File.ReadAllBytes(imagePath);

                SendImageRPC(imagePath);
            }
        });*/
    }

    public void OnSendImageButtonClicked()
    {        
       // SendImageRPC(imagePath);
    }

    [PunRPC]
    private void ReceiveImageRPC(string imagePath)
    {
  
    }
}
