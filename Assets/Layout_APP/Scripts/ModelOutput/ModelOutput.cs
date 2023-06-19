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
        // ���[�h���ꂽGameObject���擾����
        loadedGameObject = assetLoaderContext.RootGameObject;
        NameSet();
        // InteractOption�R���|�[�l���g���A�^�b�`����Ă����
        if (GetComponent<InteractOption>())
        {
            InteractOption interactOption = GetComponent<InteractOption>();



            // ���[�h���ꂽGameObject�ɃR���|�[�l���g��ǉ�����֐�
            InteractSettings(interactOption.SetBool().Combine, interactOption.SetBool().Mesh, interactOption.SetBool().ObjMani,loadedGameObject);
        }

        loadedGameObject.transform.position = SponePoint.position;
        // ���̎��_�ł̓}�e���A���ƃe�N�X�`���̓ǂݍ��݂��������Ă��Ȃ��\�������邽��
        // �I�u�W�F�N�g���\���ɂ��Ă���
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
        //�q�v�f�����Ȃ���ΏI��
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
            //�����Ώۂ̎q�I�u�W�F�g�������Ώۂɂ��ČJ��Ԃ��B
            InteractSettings(Combine, AddMesh, AddMani,ob.gameObject);
        }

    }

   

    //ObjectManipulator�̂��ݎ��ɃC�x���g��ǉ�
   

    public void BoundsStart(GameObject obj)
    {
        
        // �I�u�W�F�N�g�̃��[�J���X�P�[�����I�u�W�F�N�g�� 1m ���Ɏ��܂�悤��������
        ChangeWorldBoundsSize(MaxSize,obj);

    }

    /// <summary>
    /// ���[���h���W�ł̑S�̂̃o�E���h�T�C�Y�����Ƀ��[�J���X�P�[���𒲐�����
    /// </summary>
    public void ChangeWorldBoundsSize(float size,GameObject obj)
    {
        // ���[���h���W�̃o�E���h�T�C�Y���v�Z����
        Bounds objBounds = CalcChildObjWorldBounds(obj, new Bounds());

        // �o�E���h�̍ő咷�̕ӂ̒������擾����
        float maxlength = Mathf.Max(objBounds.size.x, objBounds.size.y, objBounds.size.z);
        Debug.Log(maxlength);
        if(maxlength < MaxSize)
        {
            return;
        }
        // �X�P�[�������̌W�����擾����
        float coefficient = size / maxlength;

        // ���[�J���X�P�[����ύX����
        obj.transform.localScale = obj.transform.localScale * coefficient;

        // ���[�J�����W�ł̃o�E���h�T�C�Y���v�Z����
       
    }


    /// <summary>
    /// �q�I�u�W�F�N�g�̃��[���h���W�ł̃o�E���h�v�Z�i�ċA�����j
    /// </summary>
    private Bounds CalcChildObjWorldBounds(GameObject obj, Bounds bounds)
    {
        // �w��I�u�W�F�N�g�̑S�Ă̎q�I�u�W�F�N�g���`�F�b�N����
        foreach (Transform child in obj.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                // �����ȃQ�[���I�u�W�F�N�g�͖�������
                continue;
            }

            // ���b�V�������_���[�̑��݊m�F
            MeshRenderer renderer = child.gameObject.GetComponent<MeshRenderer>();

            if (renderer != null)
            {
                // �t�B���^�[�̃��b�V����񂩂�o�E���h�{�b�N�X���擾����
                Bounds meshBounds = renderer.bounds;

                // �o�E���h�̃��[���h���W�ƃT�C�Y���擾����
                Vector3 meshBoundsWorldCenter = meshBounds.center;
                Vector3 meshBoundsWorldSize = meshBounds.size;

                // �o�E���h�̍ŏ����W�ƍő���W���擾����
                Vector3 meshBoundsWorldMin = meshBoundsWorldCenter - (meshBoundsWorldSize / 2);
                Vector3 meshBoundsWorldMax = meshBoundsWorldCenter + (meshBoundsWorldSize / 2);

                // �擾�����ŏ����W�ƍő���W���܂ނ悤�Ɋg��/�k�����s��
                if (bounds.size == Vector3.zero)
                {
                    // ���o�E���h�̃T�C�Y���[���̏ꍇ�̓o�E���h����蒼��
                    bounds = new Bounds(meshBoundsWorldCenter, Vector3.zero);
                }
                bounds.Encapsulate(meshBoundsWorldMin);
                bounds.Encapsulate(meshBoundsWorldMax);
            }

            // �ċA����
            bounds = CalcChildObjWorldBounds(child.gameObject, bounds);
        }
        return bounds;
    }

    /// <summary>
    /// �}�e���A���ƃe�N�X�`�����܂ޑS�Ă̓ǂݍ��݂����������Ƃ��ɌĂяo�����C�x���g
    /// </summary>
    /// <param name="assetLoaderContext"></param>
    public void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
    {
        // ���[�h���ꂽGameObject���擾����
        GameObject loadedGameObject = assetLoaderContext.RootGameObject;

        // ���̎��_�őS�Ẵ��\�[�X�̓ǂݍ��݂��������Ă���̂ŃI�u�W�F�N�g��\������
        loadedGameObject.SetActive(true);

        // �J�����̐���1m�̈ʒu�ɃI�u�W�F�N�g��z�u����
        loadedGameObject.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * 1.0f);
    }
    public void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
    {
    }

    /// <summary>
    /// �G���[�������ɌĂяo�����C�x���g
    /// </summary>
    /// <param name="contextualizedError"></param>
    public void OnError(IContextualizedError contextualizedError)
    {

    }
  
}