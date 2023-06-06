using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TriLibCore;
using TMPro;

public class ModelOutputFromAppData : ModelOutput
{
    [SerializeField, Header("�I�u�W�F�N�g�̍Œ��҃T�C�Y�����")] float ObjMaxSize;
    public void LoadFromAppData(string FileName)
    {
        MaxSize = ObjMaxSize;
        GetObjName(FileName);
        //�\���I�u�W�F�N�g�̏����ݒ�
        var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
        //�ǂݍ��ރt�@�C���p�X���쐬
        string FilrPath = Application.persistentDataPath + "/" + FileName;
       
        //���f������
        AssetLoader.LoadModelFromFile(FilrPath, OnLoad, OnMaterialsLoad, OnProgress, OnError);

    }

}