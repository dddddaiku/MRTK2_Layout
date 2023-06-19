using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TriLibCore;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using System;

[RequireComponent(typeof(InteractOption))]
public class ModelOutput : MonoBehaviour
{
    [SerializeField] Transform SponePoint;
    private GameObject loadedGameObject;
    private string ObjName;
   
    [HideInInspector] public float MaxSize;
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
        if(obj == obj.transform.root.gameObject && Combine && AddMani)
        {
           
            obj.gameObject.AddComponent<MeshCollider>();
            //obj.gameObject.GetComponent<MeshCollider>().convex = true;
            obj.gameObject.AddComponent<Rigidbody>();
            obj.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            obj.gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
            obj.gameObject.AddComponent<ObjDrag>();
            ObjDrag objDrag = obj.GetComponent<ObjDrag>();
            obj.gameObject.AddComponent<ObjectManipulator>();
            obj.gameObject.GetComponent<ObjectManipulator>().OnManipulationStarted.AddListener(objDrag.OnDrag);
            obj.gameObject.AddComponent<NearInteractionGrabbable>();
            BoundsStart(obj);
        }
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
                    //ob.gameObject.GetComponent<MeshCollider>().convex = true;
                }
                if (AddMani && !Combine)
                {
                    ob.gameObject.AddComponent<Rigidbody>();
                    ob.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    ob.gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
                    ob.gameObject.AddComponent<ObjDrag>();
                    ObjDrag objDrag = ob.GetComponent<ObjDrag>();
                    ob.gameObject.AddComponent<ObjectManipulator>();
                    ob.gameObject.GetComponent<ObjectManipulator>().OnManipulationStarted.AddListener(objDrag.OnDrag);
                    ob.gameObject.AddComponent<NearInteractionGrabbable>();
                }
            }
            //検索対象の子オブジェトを検索対象にして繰り返す。
            InteractSettings(Combine, AddMesh, AddMani,ob.gameObject);
        }

    }

   

    //ObjectManipulatorのつかみ時にイベントを追加
   

    public void BoundsStart(GameObject obj)
    {
        
        // オブジェクトのローカルスケールをオブジェクトが 1m 長に収まるよう調整する
        ChangeWorldBoundsSize(MaxSize,obj);

    }

    /// <summary>
    /// ワールド座標での全体のバウンドサイズを元にローカルスケールを調整する
    /// </summary>
    public void ChangeWorldBoundsSize(float size,GameObject obj)
    {
        // ワールド座標のバウンドサイズを計算する
        Bounds objBounds = CalcChildObjWorldBounds(obj, new Bounds());

        // バウンドの最大長の辺の長さを取得する
        float maxlength = Mathf.Max(objBounds.size.x, objBounds.size.y, objBounds.size.z);
        Debug.Log(maxlength);
        if(maxlength < MaxSize)
        {
            return;
        }
        // スケール調整の係数を取得する
        float coefficient = size / maxlength;

        // ローカルスケールを変更する
        obj.transform.localScale = obj.transform.localScale * coefficient;

        // ローカル座標でのバウンドサイズを計算する
       
    }


    /// <summary>
    /// 子オブジェクトのワールド座標でのバウンド計算（再帰処理）
    /// </summary>
    private Bounds CalcChildObjWorldBounds(GameObject obj, Bounds bounds)
    {
        // 指定オブジェクトの全ての子オブジェクトをチェックする
        foreach (Transform child in obj.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                // 無効なゲームオブジェクトは無視する
                continue;
            }

            // メッシュレンダラーの存在確認
            MeshRenderer renderer = child.gameObject.GetComponent<MeshRenderer>();

            if (renderer != null)
            {
                // フィルターのメッシュ情報からバウンドボックスを取得する
                Bounds meshBounds = renderer.bounds;

                // バウンドのワールド座標とサイズを取得する
                Vector3 meshBoundsWorldCenter = meshBounds.center;
                Vector3 meshBoundsWorldSize = meshBounds.size;

                // バウンドの最小座標と最大座標を取得する
                Vector3 meshBoundsWorldMin = meshBoundsWorldCenter - (meshBoundsWorldSize / 2);
                Vector3 meshBoundsWorldMax = meshBoundsWorldCenter + (meshBoundsWorldSize / 2);

                // 取得した最小座標と最大座標を含むように拡大/縮小を行う
                if (bounds.size == Vector3.zero)
                {
                    // 元バウンドのサイズがゼロの場合はバウンドを作り直す
                    bounds = new Bounds(meshBoundsWorldCenter, Vector3.zero);
                }
                bounds.Encapsulate(meshBoundsWorldMin);
                bounds.Encapsulate(meshBoundsWorldMax);
            }

            // 再帰処理
            bounds = CalcChildObjWorldBounds(child.gameObject, bounds);
        }
        return bounds;
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