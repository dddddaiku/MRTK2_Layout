using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSnap : MonoBehaviour
{
    [SerializeField,Header("�X�i�b�v�Ώۂ̍��W")] Vector3 _snapTargetPosition;
    [SerializeField,Header("�X�i�b�v���J�n���鋗��")] float _snapDistance;
    // Start is called before the first frame update
    public Vector3 SnapTargetPosition => _snapTargetPosition;

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
    }
    private void DistanceCheck()
    {
        if (_snapTargetPosition == null) return;
        if(Vector3.Distance(transform.position,_snapTargetPosition) <= _snapDistance)
        {
            Snap();
        }
        
    }
    private void Snap()
    {
        transform.position = _snapTargetPosition;
    }
}
