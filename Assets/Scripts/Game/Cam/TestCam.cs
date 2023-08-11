using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestCam : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cinemachineVirtualCameras;
    private int ind = 0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            ind++;
            Debug.Log(ind);
            if(ind >= cinemachineVirtualCameras.Length) ind = 0;
            for(int i = 0; i < cinemachineVirtualCameras.Length;i++){
                cinemachineVirtualCameras[i].Priority = 10;
            }
            cinemachineVirtualCameras[ind].Priority = 20;
        }
    }
}
