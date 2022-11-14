using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBaseRigidbodyController : MonoBehaviour
{

    public Transform CDG;
    protected Rigidbody Rb;

    public float WeightInLbs = 10f;
    public float Weight;

    private const float lbsToKg = 0.454f;
    private const float kgToLbs = 2.205f;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }



    // Start is called before the first frame update
    protected virtual void Start()
    {
        float finalKG = WeightInLbs * lbsToKg;
        Weight = finalKG;

        Rb.mass = Weight;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Rb)
        {
            HandlePhysics();
        }
    }

    protected virtual void HandlePhysics()
    {
        
    }
}
