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
        //表示オブジェクトの初期設定
        var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
        //読み込むファイルパスを作成
        string FilrPath = Application.persistentDataPath + "/" + FileName;
       
        //モデル召喚
        AssetLoader.LoadModelFromFile(FilrPath, OnLoad, OnMaterialsLoad, OnProgress, OnError);

    }

}