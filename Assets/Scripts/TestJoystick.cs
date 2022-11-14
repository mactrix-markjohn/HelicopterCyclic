using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestJoystick : MonoBehaviour
{

    public VariableJoystick variableJoystick;
    public GameObject helicoper;
    public Slider slider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        KeyboardHeliInput keyboardHeliInput = helicoper.GetComponent<KeyboardHeliInput>();
        
        float horizontal = variableJoystick.Horizontal;
        float vertical = variableJoystick.Vertical;

        float absHori = Mathf.Abs(horizontal);
        slider.value = absHori;
        
        keyboardHeliInput.HandleVertHori(vertical,horizontal);
        keyboardHeliInput.HandleCyclicJS();


    }
}
