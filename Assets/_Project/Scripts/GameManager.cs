using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        // Spawn the local player
        Ref.I.localPlayer = Instantiate(Ref.I.playerPrefab, new Vector3(0, 5, 0), new Quaternion(), Ref.I.world.transform);
        // Make camera follow player
        Ref.I.virtualCamera1.Follow = Ref.I.localPlayer.transform;
    }
}
