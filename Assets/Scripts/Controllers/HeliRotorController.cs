using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeliRotorController : MonoBehaviour
{

    private List<IHeliRotor> _rotors;

    private void Awake()
    {
        _rotors = GetComponentsInChildren<IHeliRotor>().ToList();
        
    }

    public void UpdateRotors(InputController inputController, float currentRPMS)
    {
        float dps = (currentRPMS * 360f) / 60f;
        for (int i = 0; i < _rotors.Count; i++)
        {
            _rotors[i].UpdateRotor(dps,inputController);
        }
    }
}
