using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSetActive : MonoBehaviour
{
    private Circle circleSetActive;
    private float circleWidthActive;
    public float CircleWidthActive { get => circleWidthActive; set => circleWidthActive = value; }

    // private float circleRadius;
    private void Start()
    {
        circleSetActive = GameObject.FindGameObjectWithTag("CirclePlayer").GetComponentInChildren<Circle>();
    }
    private void Update(){
        circleWidthActive = circleSetActive.CircleRadius*5.5f;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleWidthActive);
    }
#endif
}
