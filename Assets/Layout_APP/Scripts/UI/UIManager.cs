using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField,Header("表示を切り替えるUIを入れる。※最初に表示されるUIのみActiveにしておくこと")]
    GameObject[] Panels;
   

    //ボタンを押したときに渡された文字列と一致するUIのみを表示するクラス
    public void ChangePanel(string PanelName)
    {
        //渡された文字と同盟のUIを表示し、その他を非表示にする
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
