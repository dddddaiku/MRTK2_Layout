using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;

[RequireComponent(typeof(AzureGetBlobInfo))]

public class Layout_Core : MonoBehaviour
{
    [SerializeField] GameObject DownLoadListUI;
    [SerializeField] GameObject ViewListUI;
    
    public async void ListFromAzure()
    {
        DownLoadListUI.GetComponent<ListManager>().SetDisplayname(await gameObject.GetComponent<AzureGetBlobInfo>().ListBlobsFlatListingModel(10));
       
    }
    public void ListFromAppData()
    {
        string[] Files = Directory.GetFiles(Application.persistentDataPath);
        for(int i =0;i< Files.Length; i++)
        {
            Files[i] = Files[i].Replace(Application.persistentDataPath,"");
            Files[i] = Files[i].Remove(0, 1);
            
        }
        ViewListUI.GetComponent<ListManager>().SetDisplayname(Files);
    }
}
