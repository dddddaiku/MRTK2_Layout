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
                }
                if (AddMani)
                {
                    ob.gameObject.AddComponent<ObjectManipulator>();
                }
            }
            //�����Ώۂ̎q�I�u�W�F�g�������Ώۂɂ��ČJ��Ԃ��B
            InteractSettings(Combine, AddMesh, AddMani,ob.gameObject);
        }

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