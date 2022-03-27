using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int initialTimeRemaining;
    public int currentTimeRemaining;
    public bool gameRunning;
    
    public void StartGame()
    {
        // Spawn the world
        Ref.I.world = Instantiate(Ref.I.worldPrefab, new Vector3(), new Quaternion());
        // Spawn the local player
        Ref.I.localPlayer = Instantiate(Ref.I.playerPrefab, new Vector3(0, 1, 0), new Quaternion(), Ref.I.world.transform);
        // Get local player's lights
        for (int i = 0; i < Ref.I.localPlayer.transform.childCount; i++)
        {
            if (Ref.I.localPlayer.transform.GetChild(i).name == "Blue Light")
                Ref.I.blueLight = Ref.I.localPlayer.transform.GetChild(i).gameObject;
            else if (Ref.I.localPlayer.transform.GetChild(i).name == "Red Light")
                Ref.I.redLight = Ref.I.localPlayer.transform.GetChild(i).gameObject;
        }
        // Make camera follow player
        Ref.I.virtualCamera1.Follow = Ref.I.localPlayer.transform;
        // Start timer
        currentTimeRemaining = initialTimeRemaining;
        // Toggle game running bool
        gameRunning = true;

    }

    // Fixed update is called 50 times per second
    private void FixedUpdate()
    {
        TimeSpan t = TimeSpan.FromSeconds(Mathf.CeilToInt(currentTimeRemaining / 50));
        Ref.I.timerHUD.text = string.Format("{0:D1}:{1:D2}", t.Minutes, t.Seconds);
        Ref.I.timerEnd.text = "Time Left: " + string.Format("{0:D1}:{1:D2}", t.Minutes, t.Seconds);

        if (currentTimeRemaining > 0 && gameRunning) currentTimeRemaining -= 1;
        else if (gameRunning) EndGame();

    }


    public void EndGame()
    {
        Destroy(Ref.I.localPlayer);
        Destroy(Ref.I.world);
        Ref.I.gameHUD.SetActive(false);
        Ref.I.endMenu.SetActive(true);
        
        
        gameRunning = false;
    }


}
