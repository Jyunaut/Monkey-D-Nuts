using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public static class Inputs
    {
        public static float Horizontal => (Controller.ControlsEnabled && Controller.CanMove) ? Input.GetAxisRaw("Horizontal") : 0f;
        public static float Vertical => (Controller.ControlsEnabled && Controller.CanMove) ? Input.GetAxisRaw("Vertical") : 0f;
        public static bool InteractAPress
        {
            get
            {
                if (!Controller.ControlsEnabled || !Controller.CanInteract)
                    return false;

                return Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J);
            }
        }
        public static bool InteractBPress
        {
            get
            {
                if (!Controller.ControlsEnabled || !Controller.CanInteract)
                    return false;
                
                return Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K);
            }
        }
        public static bool InteractAHold
        {
            get
            {
                if (!Controller.ControlsEnabled || !Controller.CanInteract)
                    return false;
                
                return Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.J);
            }
        }
        public static bool InteractBHold
        {
            get
            {
                if (!Controller.ControlsEnabled || !Controller.CanInteract)
                    return false;
                
                return Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.K);
            }
        }
        public static bool IsPressingMovement
        {
            get
            {
                return Mathf.Abs(Horizontal) > 0.1f || Mathf.Abs(Vertical) > 0.1f;
            }
        }
    }
}
