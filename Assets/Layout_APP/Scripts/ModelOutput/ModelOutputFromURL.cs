using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TriLibCore;
using TMPro;

public class ModelOutputFromURL : ModelOutput
{
    [SerializeField, Header("ファイル名までの階層のURLを入力")] string URL;
    public void LoadandShowFromURL(string FileName)
    {
        GetObjName(FileName);
        //表示オブジェクトの初期設定
        var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
        //URLとファイル名を合体
        var URLPath = Path.Combine(URL, FileName);
        //URLからモデル情報を取得
        var webRequest = AssetDownloader.CreateWebRequest(URLPath);
        Debug.Log(URLPath);
        //モデル召喚
        AssetDownloader.LoadModelFromUri(webRequest, OnLoad, OnMaterialsLoad, OnProgress, OnError);
    }
}