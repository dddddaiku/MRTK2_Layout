using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractOption : MonoBehaviour
{
    [SerializeField] bool AdaptMeshCombine;
    [SerializeField] bool AdaptMeshColider;
    [SerializeField] bool AdaptObjectManipulater;
    
    public struct AdaptChecker
    {
        //表示するモデルを１つのメッシュにまとめるかのフラグ
        public bool Combine;
        //表示するモデルにメッシュコライダーを追加するかのフラグ
        public bool Mesh;
        //表示するモデルにMRTKのObjectManipulaterを追加するかのフラグ
        public bool ObjMani;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public AdaptChecker SetBool()
    {
        AdaptChecker adaptChecker;

        adaptChecker.Combine = AdaptMeshCombine;
        adaptChecker.Mesh = AdaptMeshColider;
        adaptChecker.ObjMani = AdaptObjectManipulater;
        return adaptChecker;

    }
    
}
