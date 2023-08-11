using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveCam : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cinemachineVirtualCameras;
    private float blendTime;
    public float BlendTime { get => blendTime; set => blendTime = value; }

    private void Start()
    {
        blendTime = GetComponent<CinemachineBrain>().m_DefaultBlend.BlendTime;
    }
    public void SwitchCam(int ind)
    {
        for (int i = 0; i < cinemachineVirtualCameras.Length; i++)
        {
            cinemachineVirtualCameras[i].Priority = 10;
        }
        cinemachineVirtualCameras[ind+1].Priority = 20;
    }
}
