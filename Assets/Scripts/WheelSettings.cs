using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Assets/Settings/WheelSettings")]
public class WheelSettings : ScriptableObject
{
    [SerializeField] private float _rotatePower;
    [SerializeField] private float _stopPower;

    public float RotatePower
    {
        get
        {
            return _rotatePower;
        }  set
        {
            _rotatePower = value;
        }
    }

    public float StopPower
    {
        get
        {
            return _stopPower;
        }
         set
        {
            _stopPower = value;
        }
    }
    
}
