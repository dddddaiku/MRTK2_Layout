using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Threading.Tasks;

public class SendPathToSceneObj : MonoBehaviour
{
    
    [SerializeField, Header("Manager‚Ìgameobject–¼‚ð“ü—Í")] string Manager;

    // Start is called before the first frame update
  
    public void SendURLFileName()
    {
        if (GameObject.Find(Manager)&& GameObject.Find(Manager).GetComponent<ModelOutputFromURL>())
        {
            GameObject obj = GameObject.Find(Manager);
            obj.GetComponent<ModelOutputFromURL>().LoadandShowFromURL(gameObject.GetComponent<ButtonConfigHelper>().MainLabelText);
        }
        
    }
    public void SendAppDataFileName()
    {
        if (GameObject.Find(Manager) && GameObject.Find(Manager).GetComponent<ModelOutputFromAppData>())
        {
            GameObject obj = GameObject.Find(Manager);
            obj.GetComponent<ModelOutputFromAppData>().LoadFromAppData(gameObject.GetComponent<ButtonConfigHelper>().MainLabelText);
        }

    }
    public async void SendDownLoadURLPath()
    {
        if (GameObject.Find(Manager) && GameObject.Find(Manager).GetComponent<AzureGetBlobInfo>())
        {
            Debug.Log("1");
            GameObject obj = GameObject.Find(Manager);
            Debug.Log(await obj.GetComponent<AzureGetBlobInfo>().DownloadBlob(gameObject.GetComponent<ButtonConfigHelper>().MainLabelText));
        }
    }
}
