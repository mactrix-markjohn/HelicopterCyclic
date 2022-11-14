using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRotor : MonoBehaviour, IHeliRotor
{

    public Transform LeftRotor;
    public Transform RightRotor;
    public float MaxPitch = 35f;

    
    public virtual void UpdateRotor(float dps, InputController inputController)
    {
        
    }
}
