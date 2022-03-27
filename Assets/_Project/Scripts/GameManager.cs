using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int initialTimeRemaining;
    public int currentTimeRemaining;
    public int score;
    public bool gameRunning;
    public List<string> taskInfos;
    public List<Vector3> taskLocations;




    public void StartGame()
    {
        // Reset score
        score = 0;
        // Spawn the world
        Ref.I.world = Instantiate(Ref.I.worldPrefab, new Vector3(), new Quaternion());
        // Spawn the local player
        Ref.I.localPlayer = Instantiate(Ref.I.playerPrefab, new Vector3(-0.8f, 1, -3), new Quaternion(), Ref.I.world.transform);
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
        // Dispatch the first task to the player!
        StartCoroutine(DispatchTasks(taskInfos));
        // Start timer
        currentTimeRemaining = initialTimeRemaining;
        

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

    private void Update()
    {
        Ref.I.scoreEnd.text = "Score: " + score;
        Ref.I.scoreHUD.text = "Score: " + score;
    }


    public void EndGame()
    {
        Destroy(Ref.I.localPlayer);
        Destroy(Ref.I.world);
        Ref.I.gameHUD.SetActive(false);
        Ref.I.endMenu.SetActive(true);

        DeleteTasksInGUI();
        DisableTaskAreas();
        
        
        gameRunning = false;
    }

    IEnumerator DispatchTasks(List<string> tasks)
    {
        Ref.I.gameHUD.GetComponent<AudioSource>().volume = 0f;
        Ref.I.ringtone.Play();
        yield return new WaitForSeconds(Ref.I.ringtone.clip.length);
        foreach (string task in tasks)
        {
            GameObject taskGameObject = Instantiate(Ref.I.taskPrefab, Ref.I.tasking.transform);
            float length = Ref.I.murmurs.playRandomSound();
            StartCoroutine(SlowType(task, taskGameObject.transform.GetChild(1).GetComponent<TMP_Text>(), length/task.Length));
            yield return new WaitForSeconds(length);
        }
        Ref.I.gameHUD.GetComponent<AudioSource>().volume = 0.5f;
        // Toggle game running bool
        gameRunning = true;
        EnableTaskAreas();
    }

    IEnumerator SlowType(string s, TMP_Text tMP_Text, float timeBetweenChars)
    {
        foreach (char c in s)
        {
            yield return new WaitForSeconds(timeBetweenChars);
            tMP_Text.text += c;
        }
    }


    public void DeleteTasksInGUI()
    {
        for (int i = 0; i < Ref.I.tasking.transform.childCount; i++)
        {
            if (Ref.I.tasking.transform.GetChild(i).gameObject.name.Contains("Task"))
                Destroy(Ref.I.tasking.transform.GetChild(i).gameObject);
        }
    }

    public void EnableTaskAreas()
    {
        Ref.I.world.transform.GetChild(9).GetChild(0).gameObject.SetActive(true);
        Ref.I.world.transform.GetChild(9).GetChild(1).gameObject.SetActive(true);
        Ref.I.world.transform.GetChild(9).GetChild(2).gameObject.SetActive(true);
        Ref.I.world.transform.GetChild(9).GetChild(3).gameObject.SetActive(true);
    }

    public void DisableTaskAreas()
    {
        Ref.I.world.transform.GetChild(9).GetChild(0).gameObject.SetActive(false);
        Ref.I.world.transform.GetChild(9).GetChild(1).gameObject.SetActive(false);
        Ref.I.world.transform.GetChild(9).GetChild(2).gameObject.SetActive(false);
        Ref.I.world.transform.GetChild(9).GetChild(3).gameObject.SetActive(false);
    }


}
