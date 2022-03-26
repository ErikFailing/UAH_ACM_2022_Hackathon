using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 move;
    public int movementMultiplier;
    public int turningMultiplier;

    private void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (Ref.I.localPlayer != null)
        {
            // Forward and backward movement
            Ref.I.localPlayer.GetComponent<Rigidbody>().AddForce(Ref.I.localPlayer.transform.forward * (move.y * movementMultiplier));

            // Side to side turning
            Ref.I.localPlayer.GetComponent<Rigidbody>().AddTorque(Ref.I.localPlayer.transform.up * move.x * turningMultiplier);
        }
        
        
    }
}
