using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Circle : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerManager;
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    // private FollowCam followCam;
    private float circleRadius;
    private int levelCircle;

    public float CircleRadius { get => circleRadius; set => circleRadius = value; }
    public int LevelCircle { get => levelCircle; set => levelCircle = value; }

    private void Awake()
    {
        // Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        // if(brain == null){
        //     brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        // }
        // brain.m_DefaultBlend.m_Time = 1;
        // brain.m_ShowDebugText = true;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        cinemachineVirtualCamera = GameObject.FindGameObjectWithTag("CinemachineVirtualCamera").GetComponent<CinemachineVirtualCamera>();
        circleRadius = GetComponent<Renderer>().bounds.size.x / 2;
        levelCircle = Prefs.radiusCircle;
        GetCircleRadius();
    }
    private void Update()
    {
        // circleRadius = GetComponent<Renderer>().bounds.size.x / 2;
    }
    // public float GetCircleLength()
    // {
    //     Vector3 radiusCircle = GetComponent<Renderer>().bounds.size;
    //     return radiusCircle.x / 2;
    // }
    public void UpdateCircleRadius()
    {
        levelCircle++;
        GetCircleRadius();
        // followCam.DirCam = new Vector3(followCam.DirCam.x, followCam.DirCam.y+0.55f, followCam.DirCam.z-0.3f);
    }
    public void GetCircleRadius(){
        // Debug.Log(levelCircle);
        gameObject.transform.localScale = new Vector3(
            Config.radiusInitial.x + (0.01f * levelCircle),
            Config.radiusInitial.y + (0.01f * levelCircle),
            Config.radiusInitial.z
        );
        circleRadius = GetComponent<Renderer>().bounds.size.x / 2;
        cinemachineVirtualCamera.m_Lens.FieldOfView = 60 + 0.4f * levelCircle;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
#endif
}

