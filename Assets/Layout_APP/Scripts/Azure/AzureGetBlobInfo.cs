using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure;
using System;
//参考ドキュメント→https://learn.microsoft.com/ja-jp/azure/storage/blobs/storage-blobs-list
//Blobのアクセスレベルがprivateな場合は取得できない
/// <summary>
/// Azureサーバーに上がっているコンテナ内のBlobを参照しローカルに保存するクラス
/// </summary>
public class AzureGetBlobInfo : MonoBehaviour
{
    /// <summary>
    /// モデルコンテナ名
    /// </summary>
    [SerializeField]
    private string ContainerModel;
    /// <summary>
    /// モデル名管理リスト
    /// </summary>
    [HideInInspector]
    private List<string> modelNames;
    [SerializeField]
    private string SetconnectionString;
    private BlobServiceClient blobServiceClient;
    void Awake()
    {
        //モデル名管理リスト初期化
        modelNames = new List<string>();
    }

   
    /// <summary>
    /// public実装
    /// 3Dモデルを格納しているコンテナ内BLOBファイルを全て参照し、ローカルに保存する非同期処理関数
    /// </summary>
    /// <param name="blobServiceClient">任意に指定したblobServiceClient</param>
    /// <param name="segmentSize">ファイル数（メモリ確保）</param>
    /// <returns></returns>
    public async Task<string[]> ListBlobsFlatListingModel(int? segmentsize)
    {
        try
        {
            blobServiceClient = new BlobServiceClient(SetconnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerModel);

            //コンテナ参照

            // リスティングオペレーションを呼び出し、指定されたサイズのページを返す
            var resultSegment = containerClient.GetBlobsAsync().AsPages(default, segmentsize);


            modelNames.Clear();
            Debug.Log(resultSegment.ToString());

            // 各ページで返されるblobを列挙
            await foreach (Page<BlobItem> blobPage in resultSegment)
            {

                //blonの数分回し、blobItemから参照する
                foreach (BlobItem blobItem in blobPage.Values)
                {

                    //Blob情報取得,Model名をClientに保存
                    BlobClient blobClient = containerClient.GetBlobClient(blobItem.Name);

                    modelNames.Add(blobItem.Name);

                    //保存先を指定し名前付けを行う(任意指定)
                    //string savePath = csvPath + blobItem.Name;
                    //指定したパスのローカルに保存する
                    //await blobClient.DownloadToAsync(savePath);
                    Debug.Log("取得したModelファイル" + blobItem.Name);
                }
            }
            return modelNames.ToArray();
        }
        //catch (RequestFailedException e)
        catch (Exception e)
        {
            e.ToString();
            Debug.Log("読み込みが失敗しました");
            throw;
        }
    }
    public void DownLoadModelData(string FileName)
    {

    }
    public async Task<string> DownloadBlob(string FileName)
    {
        try
        {
            blobServiceClient = new BlobServiceClient(SetconnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerModel);

                    BlobClient blobClient = containerClient.GetBlobClient(FileName);

            Debug.Log(blobClient.Name);

                    //保存先を指定し名前付けを行う(任意指定)
                    string savePath = Application.persistentDataPath +"/"+ FileName;
                    //指定したパスのローカルに保存する
                    await blobClient.DownloadToAsync(savePath);

            return "ダウンロード完了";
        }
        //catch (RequestFailedException e)
        catch (Exception e)
        {
            e.ToString();
            Debug.Log("読み込みが失敗しました");
            throw;
        }
    }
}