using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHeliInput : BaseHeliInput
{
   public void HandleThrottle(float throttleInput)
   {
      ThrottleInput = throttleInput;
   }

   
   //TODO: Take note of HandlePedal and HandleCollective, both are switching functions

   public void HandlePedal(float pedalInput)
   {
      PedalInput = pedalInput;
   }

   public void HandleCollective(float collecttiveInput)
   {
      CollectiveInput = collecttiveInput;
   }

   public void HandleCyclic()
   {
      Vector2 temp = new Vector2(Horizontal, Vertical);
      CyclicInput = temp;
   }
   
   public void HandleVerHorTouch(float vertical, float horizontal)
   {

      Vertical = vertical;
      Horizontal = horizontal;
        
   }
}
