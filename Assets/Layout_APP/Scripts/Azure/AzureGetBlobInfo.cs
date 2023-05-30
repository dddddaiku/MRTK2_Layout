using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure;
using System;
//�Q�l�h�L�������g��https://learn.microsoft.com/ja-jp/azure/storage/blobs/storage-blobs-list
//Blob�̃A�N�Z�X���x����private�ȏꍇ�͎擾�ł��Ȃ�
/// <summary>
/// Azure�T�[�o�[�ɏオ���Ă���R���e�i����Blob���Q�Ƃ����[�J���ɕۑ�����N���X
/// </summary>
public class AzureGetBlobInfo : MonoBehaviour
{
    /// <summary>
    /// ���f���R���e�i��
    /// </summary>
    [SerializeField]
    private string ContainerModel;
    /// <summary>
    /// ���f�����Ǘ����X�g
    /// </summary>
    [HideInInspector]
    private List<string> modelNames;
    [SerializeField]
    private string SetconnectionString;
    private BlobServiceClient blobServiceClient;
    void Awake()
    {
        //���f�����Ǘ����X�g������
        modelNames = new List<string>();
    }

   
    /// <summary>
    /// public����
    /// 3D���f�����i�[���Ă���R���e�i��BLOB�t�@�C����S�ĎQ�Ƃ��A���[�J���ɕۑ�����񓯊������֐�
    /// </summary>
    /// <param name="blobServiceClient">�C�ӂɎw�肵��blobServiceClient</param>
    /// <param name="segmentSize">�t�@�C�����i�������m�ہj</param>
    /// <returns></returns>
    public async Task<string[]> ListBlobsFlatListingModel(int? segmentsize)
    {
        try
        {
            blobServiceClient = new BlobServiceClient(SetconnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerModel);

            //�R���e�i�Q��

            // ���X�e�B���O�I�y���[�V�������Ăяo���A�w�肳�ꂽ�T�C�Y�̃y�[�W��Ԃ�
            var resultSegment = containerClient.GetBlobsAsync().AsPages(default, segmentsize);


            modelNames.Clear();
            Debug.Log(resultSegment.ToString());

            // �e�y�[�W�ŕԂ����blob���
            await foreach (Page<BlobItem> blobPage in resultSegment)
            {

                //blon�̐����񂵁AblobItem����Q�Ƃ���
                foreach (BlobItem blobItem in blobPage.Values)
                {

                    //Blob���擾,Model����Client�ɕۑ�
                    BlobClient blobClient = containerClient.GetBlobClient(blobItem.Name);

                    modelNames.Add(blobItem.Name);

                    //�ۑ�����w�肵���O�t�����s��(�C�ӎw��)
                    //string savePath = csvPath + blobItem.Name;
                    //�w�肵���p�X�̃��[�J���ɕۑ�����
                    //await blobClient.DownloadToAsync(savePath);
                    Debug.Log("�擾����Model�t�@�C��" + blobItem.Name);
                }
            }
            return modelNames.ToArray();
        }
        //catch (RequestFailedException e)
        catch (Exception e)
        {
            e.ToString();
            Debug.Log("�ǂݍ��݂����s���܂���");
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

                    //�ۑ�����w�肵���O�t�����s��(�C�ӎw��)
                    string savePath = Application.persistentDataPath +"/"+ FileName;
                    //�w�肵���p�X�̃��[�J���ɕۑ�����
                    await blobClient.DownloadToAsync(savePath);

            return "�_�E�����[�h����";
        }
        //catch (RequestFailedException e)
        catch (Exception e)
        {
            e.ToString();
            Debug.Log("�ǂݍ��݂����s���܂���");
            throw;
        }
    }
}