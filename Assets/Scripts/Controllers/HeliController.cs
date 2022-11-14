using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(InputController), typeof(HelicopterCharacteristics))]
public class HeliController : HelicopterBaseRigidbodyController
{

    [Header("Helicopter Properties")] 
    private InputController _inputController;
    private List<HeliEngine> _engines = new List<HeliEngine>();

    [Header("Heliicopter Rotors")] 
    private HeliRotorController _rotorController;
    private HelicopterCharacteristics _helicopterCharacteristics;

    public List<HeliEngine> Engines
    {
        get { return _engines; }
    }

    protected override void Awake()
    {
        base.Awake();
        _inputController = GetComponent<InputController>();
        _rotorController = GetComponentInChildren<HeliRotorController>();
        _engines = GetComponentsInChildren<HeliEngine>().ToList();
        _helicopterCharacteristics = GetComponent<HelicopterCharacteristics>();
        
    }

    protected override void HandlePhysics()
    {
        base.HandlePhysics();
        if (_inputController)
        {

            HandleEngines();
            HandleRotors();
            HandleCharacteristics();

        }
    }

    private void HandleEngines()
    {
        for (int i = 0; i < _engines.Count; i++)
        {
            _engines[i].UpdateEngine(_inputController.CurrentInput.StickyThrottle);
            float finalPower = _engines[i].CurrentHP;
        }
    }

    private void HandleRotors()
    {

        if (_rotorController && _engines.Count > 0)
        {
            _rotorController.UpdateRotors(_inputController, _engines[0].CurrentRPM);
        }
    }

    private void HandleCharacteristics()
    {
        if (_helicopterCharacteristics)
        {
            _helicopterCharacteristics.HandleCharacteristics(Rb, _inputController);
        }
    }
}
