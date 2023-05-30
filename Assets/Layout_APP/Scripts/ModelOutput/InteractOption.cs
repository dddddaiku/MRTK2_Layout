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
        //�\�����郂�f�����P�̃��b�V���ɂ܂Ƃ߂邩�̃t���O
        public bool Combine;
        //�\�����郂�f���Ƀ��b�V���R���C�_�[��ǉ����邩�̃t���O
        public bool Mesh;
        //�\�����郂�f����MRTK��ObjectManipulater��ǉ����邩�̃t���O
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
