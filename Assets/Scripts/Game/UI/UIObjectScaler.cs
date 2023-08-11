using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectScaler : MonoBehaviour
{
    float mainRatio = 9f / 16f;
    // Start is called before the first frame update
    void Start()
    {
        float currentRatio = Camera.main.aspect;
        float deltaRatio = currentRatio / mainRatio;
    }
}
