using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterCharacteristics : MonoBehaviour
{
    [Header("Lift Properties")] 
    public float MaxLifeForce = 100f;

    public float hardcordLiftForce = 100f;
    
    private HeliController _heliControl;

    [Space] 
    [Header("Tail Rotor Properties")]
    public float TailForce = 2f;

    [Space] 
    [Header("Cyclic Properties")] 
    public float CyclicForce = 2f;
    public float CyclicForceMultiplier = 1000f;

    [Space] 
    [Header("Auto Level Properties")]
    public float AutoLevelForce = 2f;

    private Vector3 _flatForward;
    private float _forwardDot;
    private Vector3 _flatRight;
    private float _rightDot;

    protected virtual void Awake()
    {
        _heliControl = GetComponent<HeliController>();
    }

    public virtual void HandleCharacteristics(Rigidbody rb, InputController input)
    {
        HandleLife(rb,input);
        HandleCyclic(rb, input);
        HandlePedals(rb, input);

        CalculateAngles();
        AutoLevel(rb);
    }

    private void HandleLife(Rigidbody rb, InputController input)
    {
        Vector3 liftForce = transform.up *(UnityEngine.Physics.gravity.magnitude + MaxLifeForce) * rb.mass;
        float normalizedRPMs = _heliControl.Engines[0].NormalizedRPM;
        liftForce *= Mathf.Pow(input.CurrentInput.StickyCollective, 2) * Mathf.Pow(normalizedRPMs, 2f);

        liftForce = transform.up * hardcordLiftForce; // This is hardcoded value, remove it once done with testing
        rb.AddForce(liftForce,ForceMode.Force);
        
    }

    private void HandleCyclic(Rigidbody rb, InputController input)
    {
        //handle cyclic
        float cyclicXForce = -input.CurrentInput.CyclicInput.x * CyclicForce;
        rb.AddRelativeTorque(Vector3.forward * cyclicXForce, ForceMode.Acceleration);

        float cyclicYForce = input.CurrentInput.CyclicInput.y * CyclicForce;
        rb.AddRelativeTorque(Vector3.right * cyclicYForce, ForceMode.Acceleration);
        
        //apply force based off of the dot product value

        Vector3 forwardVector = _flatForward * _forwardDot;
        Vector3 rightVector = _flatRight * _rightDot;
        Vector3 finalCyclicDir = forwardVector + rightVector;
        finalCyclicDir = Vector3.ClampMagnitude(finalCyclicDir, 1f) * CyclicForce * CyclicForceMultiplier;
        
        rb.AddForce(finalCyclicDir, ForceMode.Force);

    }

    private void HandlePedals(Rigidbody rb, InputController input)
    {
        // handle taill rotors
        
        rb.AddTorque(Vector3.up * input.CurrentInput.PedalInput * TailForce, ForceMode.Acceleration);
    }

    private void CalculateAngles()
    {
        //calculate flat forward
        _flatForward = transform.forward;
        _flatForward.y = 0f;
        _flatForward = _flatForward.normalized;
        
        //calculate flat right
        _flatRight = transform.right;
        _flatRight.y = 0f;
        _flatRight = _flatRight.normalized;
        
        //calculate angles (dot products)
        _forwardDot = Vector3.Dot(transform.up, _flatForward);
        _rightDot = Vector3.Dot(transform.up, _flatRight);
    }

    private void AutoLevel(Rigidbody rb)
    {
        // auto correct the helicopter

        float rightForce = -_forwardDot * AutoLevelForce;
        float forwardForce = _rightDot * AutoLevelForce;
        
        rb.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
        rb.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
    }
}
