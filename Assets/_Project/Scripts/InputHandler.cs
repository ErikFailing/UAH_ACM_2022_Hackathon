using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 move;
    public int movementMultiplier;
    public int turningMultiplier;
    public float boost;
    public float boostMultiplier;
    public int lightSwitchDelay;

    private double physicsFramesSinceLastLightSwitch;

    

    private void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    private void OnBoost(InputValue value)
    {
        boost = value.Get<float>();
    }

    private void FixedUpdate()
    {
        if (Ref.I.localPlayer != null)
        {
            // Forward and backward movement
            Ref.I.localPlayer.GetComponent<Rigidbody>().AddForce(
                boost > 0? Ref.I.localPlayer.transform.forward * (move.y * movementMultiplier * boostMultiplier) : 
                Ref.I.localPlayer.transform.forward * (move.y * movementMultiplier));
            
            // Side to side turning
            Ref.I.localPlayer.GetComponent<Rigidbody>().AddTorque(Ref.I.localPlayer.transform.up * move.x * turningMultiplier);

            // Turn on weewoos and lights if boosting
            if (boost > 0)
            {
                // Flash lights
                if (physicsFramesSinceLastLightSwitch > lightSwitchDelay)
                {
                    if (!Ref.I.blueLight.activeInHierarchy)
                    {
                        Ref.I.blueLight.SetActive(true);
                        Ref.I.redLight.SetActive(false);
                    }
                    else
                    {
                        Ref.I.blueLight.SetActive(false);
                        Ref.I.redLight.SetActive(true);
                    }
                    physicsFramesSinceLastLightSwitch = 0;
                }
                else physicsFramesSinceLastLightSwitch++;
            }
            else
            {
                // Turn off lights
                Ref.I.blueLight.SetActive(false);
                Ref.I.redLight.SetActive(false);

            }

            

        }
        
        
    }
}
