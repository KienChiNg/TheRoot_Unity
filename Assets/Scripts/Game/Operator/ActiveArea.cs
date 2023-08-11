using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class ActiveArea : MonoBehaviour
{
    [SerializeField] private float Range;

    public float Range1 { get => Range; set => Range = value; }
    // private void OnBecameInvisible()
    // {
    //     transform.parent.GetChild(1).gameObject.SetActive(false);
    //     // Debug.Log(transform.parent.GetChild(1).gameObject.name);
    // }
    // private void OnBecameVisible()
    // {
    //     transform.parent.GetChild(1).gameObject.SetActive(true);
    // }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        while (true)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, Range, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        // result = Vector3.zero;
        // return false;
    }

    public Vector3 GetRandomPoint(Transform point = null, float radius = 0)
    {
        Vector3 randomDirection = transform.position + Random.insideUnitSphere * Range;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, Range, NavMesh.AllAreas))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
#endif
}
