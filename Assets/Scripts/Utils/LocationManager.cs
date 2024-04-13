using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private float range = 10;
    
    
    public Vector3 GetRandomPointOnNavMesh(Transform originTransform)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += originTransform.position;
        randomDirection.z = 0;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            Debug.Log($"Failed to find NavMesh point. Tried at: {randomDirection} with range: {range}");
            return Vector3.zero;
        }
    }
}
