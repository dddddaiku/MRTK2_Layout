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

    //使用するボタンの数の合わせる
    [SerializeField] GameObject[] Buttons;
    [HideInInspector] string[] DisplayNames;
    [HideInInspector] int Pages;


  
    // Start is called before the first frame update
    void Start()
    {
        //ページの初期知設定
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
   　private void ListSettings()
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
        //ページ数表記の更新
        PageDisplay.GetComponent<TMP_Text>().text = Pages.ToString();
    }
   
   
    /// <summary>
    /// 外部から文字列を投げ入れるクラス
    /// </summary>
    /// <param name="Names">リストに表示する文字配列</param>
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
        //Nextボタンから呼び出し
        Pages++;
        ListUpdate();
    }
    public void PageDown()
    {
        //Backボタンから呼び出し
        Pages--;
        ListUpdate();
    }
}
