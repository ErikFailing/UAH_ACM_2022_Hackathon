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
    public int flipStrength;
    public int upStrength;

    private double physicsFramesSinceLastLightSwitch;
    private bool boostEnabled;
    private bool flip;

    

    private void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    private void OnBoost(InputValue value)
    {
        boost = value.Get<float>();
    }

    private void OnFlip(InputValue value)
    {
        flip = true;
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

            if (flip)
            {
                Ref.I.localPlayer.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * upStrength, ForceMode.Impulse);
                Ref.I.localPlayer.GetComponent<Rigidbody>().AddTorque(Ref.I.localPlayer.transform.forward * flipStrength, ForceMode.Impulse);
                flip = false;
            }

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
                // Turn on weewoos
                if (!boostEnabled)
                {
                    int i = Random.Range(0, 3);
                    if (i == 0) Ref.I.weeWoo1.SetActive(true);
                    else if (i == 1) Ref.I.weeWoo2.SetActive(true);
                    else if (i == 2) Ref.I.weeWoo3.SetActive(true);
                    boostEnabled = true;
                }
            }
            else
            {
                // Turn off lights
                Ref.I.blueLight.SetActive(false);
                Ref.I.redLight.SetActive(false);
                // Turn off weewoo
                Ref.I.weeWoo1.SetActive(false);
                Ref.I.weeWoo2.SetActive(false);
                Ref.I.weeWoo3.SetActive(false);
                boostEnabled = false;
            }

            

        }
        
        
    }
}
