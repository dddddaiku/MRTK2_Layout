using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField,Header("�\����؂�ւ���UI������B���ŏ��ɕ\�������UI�̂�Active�ɂ��Ă�������")]
    GameObject[] Panels;
   

    //�{�^�����������Ƃ��ɓn���ꂽ������ƈ�v����UI�݂̂�\������N���X
    public void ChangePanel(string PanelName)
    {
        //�n���ꂽ�����Ɠ�����UI��\�����A���̑����\���ɂ���
        for (int i = 0; i < Panels.Length; i++)
        {
            if(Panels[i].name == PanelName)
            {
                Panels[i].SetActive(true);
                
            }
            else
            {
                Panels[i].SetActive(false);
            }
        }
    }
}
