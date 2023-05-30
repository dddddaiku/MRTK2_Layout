using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TriLibCore;
using TMPro;

public class ModelOutputFromAppData : ModelOutput
{
    public void LoadFromAppData(string FileName)
    {
        GetObjName(FileName);
        //�\���I�u�W�F�N�g�̏����ݒ�
        var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
        //�ǂݍ��ރt�@�C���p�X���쐬
        string FilrPath = Application.persistentDataPath + "/" + FileName;
       
        //���f������
        AssetLoader.LoadModelFromFile(FilrPath, OnLoad, OnMaterialsLoad, OnProgress, OnError);

    }

}