using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class ListManager : MonoBehaviour
{
    [SerializeField] GameObject NextButton;
    [SerializeField] GameObject BackButton;
    [SerializeField] GameObject PageDisplay;

    //�g�p����{�^���̐��̍��킹��
    [SerializeField] GameObject[] Buttons;
    [HideInInspector] string[] DisplayNames;
    [HideInInspector] int Pages;


  
    // Start is called before the first frame update
    void Start()
    {
        //�y�[�W�̏����m�ݒ�
        Pages = 1;

        for(int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }
        NextButton.SetActive(false);
        BackButton.SetActive(false);
         ListUpdate();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void ListUpdate()
    {
        ListSettings();
        PageSettings();
    }
   �@private void ListSettings()
    {
        for(int i = 0; i < Buttons.Length; i++)
        {
            if(DisplayNames.Length <= Buttons.Length * (Pages-1) + i)
            {
                Buttons[i].SetActive(false);
               
            }
            else
            {
                Buttons[i].SetActive(true);
                Buttons[i].GetComponent<ButtonConfigHelper>().MainLabelText = DisplayNames[Buttons.Length * (Pages - 1) + i];
            }
        }
    }
    private void PageSettings()
    {
        if(DisplayNames.Length <= Buttons.Length)
        {
            NextButton.SetActive(false);
            BackButton.SetActive(false);
        }
        else if(Pages == 1)
        {
            NextButton.SetActive(true);
            BackButton.SetActive(false);
        }
        else if(Pages > DisplayNames.Length/ Buttons.Length)
        {
            NextButton.SetActive(false);
            BackButton.SetActive(true);
        }
        else
        {
            NextButton.SetActive(true);
            BackButton.SetActive(true);
        }
        //�y�[�W���\�L�̍X�V
        PageDisplay.GetComponent<TMP_Text>().text = Pages.ToString();
    }
   
   
    /// <summary>
    /// �O�����當����𓊂������N���X
    /// </summary>
    /// <param name="Names">���X�g�ɕ\�����镶���z��</param>
    public void SetDisplayname(string[] Names)
    {
        DisplayNames = new string[Names.Length];
        //FileNames = Names;
        for(int i = 0; i < Names.Length; i++)
        {
            DisplayNames[i] = Names[i];
        }
        ListUpdate();
    }
    public void PageUP()
    {
        //Next�{�^������Ăяo��
        Pages++;
        ListUpdate();
    }
    public void PageDown()
    {
        //Back�{�^������Ăяo��
        Pages--;
        ListUpdate();
    }
}
