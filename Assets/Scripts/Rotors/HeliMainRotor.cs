using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliMainRotor : BaseRotor
{

   public float MinRotationRRPM = 40f;

   public override void UpdateRotor(float dps, InputController inputController)
   {
      //base.UpdateRotor(dps, inputController);

      dps = Mathf.Clamp(dps, 0f, MinRotationRRPM);
      transform.Rotate(Vector3.right, dps);
      
      // pitch the blades up and down
      /*if (LeftRotor && RightRotor)
      {

         LeftRotor.localRotation = Quaternion.Euler(
            inputController.CurrentInput.StickyCollective * MaxPitch, 0f, 0f);
         RightRotor.localRotation = Quaternion.Euler(
            -inputController.CurrentInput.StickyCollective * MaxPitch, 0f, 0f);

      }*/
   }
}
