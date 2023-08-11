using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private Vector3 dirCam;

    public Vector3 DirCam { get => dirCam; set => dirCam = value; }

    // Update is called once per frame
    void LateUpdate()
    {
        cam.transform.position = transform.position + dirCam;
    }
}
