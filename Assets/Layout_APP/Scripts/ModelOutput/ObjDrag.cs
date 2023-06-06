using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ObjDrag : MonoBehaviour
{
    public void OnDrag(ManipulationEventData arg0)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
