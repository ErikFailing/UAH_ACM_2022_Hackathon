using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskLocation : MonoBehaviour
{
    public int taskID;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            // Task Location entered
            Ref.I.tasking.transform.GetChild(taskID+1).gameObject.GetComponent<Toggle>().isOn = true;
            Ref.I.tasking.transform.GetChild(taskID+1).gameObject.GetComponent<Toggle>().GraphicUpdateComplete();
            //Ref.I.tasking.transform.GetChild(taskID+1).gameObject.GetComponent<Toggle>().Select();

            Ref.I.gameManager.score += 1;
            if (Ref.I.gameManager.score == 4) Ref.I.gameManager.EndGame();
            gameObject.SetActive(false);

            Debug.Log("Player entered task location");

        }
    }

}
