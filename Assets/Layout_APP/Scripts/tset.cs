using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class tset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("1");
        gameObject.GetComponent<ObjectManipulator>().OnManipulationStarted.AddListener(Tes);

        Debug.Log("2");
    }

    private void Tes(ManipulationEventData arg0)
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    // Update is called once per frame



   
}
