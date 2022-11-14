using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardHeliInput : BaseHeliInput
{
    protected override void HandleThrottle()
    {
        base.HandleThrottle();
        ThrottleInput = Input.GetAxis("Throttle");
    }

    //TODO: Take note of HandlePedal and HandleCollective, both are switching functions
    protected override void HandlePedal()
    {
        base.HandlePedal();
        CollectiveInput = Input.GetAxis("Collective");
    }

    protected override void HandleCollective()
    {
        base.HandleCollective();
        PedalInput = Input.GetAxis("Pedal");
    }

    protected override void HandleCyclic()
    {
        base.HandleCyclic();
        Vector2 temp = new Vector2(Horizontal, Vertical);

        CyclicInput = temp;
    }

    public void HandleCyclicJS()
    {
         Vector2 temp = new Vector2(Horizontal, Vertical);
         CyclicInput = temp;
    }

    public void HandleVertHori(float vertical, float horizontal)
    {
        Horizontal = horizontal;
        Vertical = vertical;
    }

}
