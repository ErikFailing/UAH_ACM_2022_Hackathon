using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ref : MonoBehaviour
{
    public static Ref I { get; private set; }
    private void Awake()
    {
        // Make Ref a singleton
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(this);

    }

    [Header("Refs set before runtime")]
    public Camera mainCamera;
    public CinemachineVirtualCamera virtualCamera1;
    public GameObject playerPrefab;
    public GameObject worldPrefab;
    public TMP_Text timerHUD;
    public TMP_Text timerEnd;
    public TMP_Text scoreHUD;
    public TMP_Text scoreEnd;
    public GameObject mainMenu;
    public GameObject gameHUD;
    public GameObject endMenu;
    


    [Header("Refs set during runtime")]
    public GameObject world;
    public GameObject localPlayer;
    public GameObject blueLight;
    public GameObject redLight;

}
