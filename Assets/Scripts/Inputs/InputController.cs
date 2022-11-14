using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(KeyboardHeliInput))]
[RequireComponent(typeof(TouchHeliInput))]
public class InputController : MonoBehaviour
{
    public InputType Input = InputType.PC;
    private TouchHeliInput _touchController;
    private KeyboardHeliInput _pcController;
    
    private List<BaseHeliInput> _inputs = new List<BaseHeliInput>();
    
    private BaseHeliInput _currentInput;

    public BaseHeliInput CurrentInput
    {
        get { return _currentInput; }
    }

    private void Awake()
    {
        _inputs = GetComponents<BaseHeliInput>().ToList();
        _touchController = GetComponent<TouchHeliInput>();
        _pcController = GetComponent<KeyboardHeliInput>();

        SetInputType();
    }

    private void SetInputType()
    {
        DisableAllInputs();

        switch (Input)
        {
            
            case InputType.PC:
                _pcController.enabled = true;
                _currentInput = _pcController;
                break;
            
            case InputType.TOUCH:
                _touchController.enabled = true;
                _currentInput = _touchController;
                break;
            
            default:
                _touchController.enabled = true;
                _currentInput = _touchController;
                break;
        }
    }

    private void DisableAllInputs()
    {

        foreach (BaseHeliInput input in _inputs)
        {
            input.enabled = false;
        }
        
    }
}
