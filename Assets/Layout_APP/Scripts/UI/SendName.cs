using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SendName : MonoBehaviour
{
    [SerializeField] string TargetButtonConfigHelperName;

    public void SetName()
    {
        GameObject TargetButtonConfigHelper = GameObject.Find(TargetButtonConfigHelperName);
        TargetButtonConfigHelper.GetComponent<ButtonConfigHelper>().MainLabelText = gameObject.gameObject.GetComponent<ButtonConfigHelper>().MainLabelText;
    }
}
