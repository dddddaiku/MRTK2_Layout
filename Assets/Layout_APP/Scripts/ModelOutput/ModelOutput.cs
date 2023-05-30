using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TriLibCore;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

[RequireComponent(typeof(InteractOption))]
public class ModelOutput : MonoBehaviour
{
    [SerializeField] Transform SponePoint;
    private GameObject loadedGameObject;
    private string ObjName;
    // Start is called before the first frame update


    public void OnLoad(AssetLoaderContext assetLoaderContext)
    {
        // ロードされたGameObjectを取得する
        loadedGameObject = assetLoaderContext.RootGameObject;
        NameSet();
        // InteractOptionコンポーネントがアタッチされていれば
        if (GetComponent<InteractOption>())
        {
            InteractOption interactOption = GetComponent<InteractOption>();

            // ロードされたGameObjectにコンポーネントを追加する関数
            InteractSettings(interactOption.SetBool().Combine, interactOption.SetBool().Mesh, interactOption.SetBool().ObjMani,loadedGameObject);
        }

        loadedGameObject.transform.position = SponePoint.position;
        // この時点ではマテリアルとテクスチャの読み込みが完了していない可能性があるため
        // オブジェクトを非表示にしておく
        loadedGameObject.SetActive(false);
    }
    public void GetObjName(string name)
    {
        ObjName = name;
    }
    public void NameSet()
    {
        loadedGameObject.name = ObjName;
    }
    public void InteractSettings(bool Combine, bool AddMesh, bool AddMani,GameObject obj)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            if (!ob.gameObject.GetComponent<MeshFilter>())
            {

            }
            else
            {
                if (AddMesh)
                {
                    ob.gameObject.AddComponent<MeshCollider>();
                }
                if (AddMani)
                {
                    ob.gameObject.AddComponent<ObjectManipulator>();
                }
            }
            //検索対象の子オブジェトを検索対象にして繰り返す。
            InteractSettings(Combine, AddMesh, AddMani,ob.gameObject);
        }

    }
    

        /// <summary>
        /// マテリアルとテクスチャを含む全ての読み込みが完了したときに呼び出されるイベント
        /// </summary>
        /// <param name="assetLoaderContext"></param>
        public void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
    {
        // ロードされたGameObjectを取得する
        GameObject loadedGameObject = assetLoaderContext.RootGameObject;

        // この時点で全てのリソースの読み込みが完了しているのでオブジェクトを表示する
        loadedGameObject.SetActive(true);

        // カメラの正面1mの位置にオブジェクトを配置する
        loadedGameObject.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 1.0f);
    }
    public void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
    {
    }

    /// <summary>
    /// エラー発生時に呼び出されるイベント
    /// </summary>
    /// <param name="contextualizedError"></param>
    public void OnError(IContextualizedError contextualizedError)
    {

    }
  
}