using Cinemachine;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject world;
    


    [Header("Refs set during runtime")]
    public GameObject localPlayer;

}
