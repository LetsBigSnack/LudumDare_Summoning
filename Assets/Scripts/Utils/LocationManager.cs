using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LocationManager : MonoBehaviour
{
    [SerializeField] private float range = 10;
    [SerializeField] private float _checkRadius = 1.5f;
    public List<Collider2D> spawningAreas;
    
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


    public Vector3 GetRandomPointOnSpawnArea()
    {
        int index = SelectRandomArea();
        return SelectRandomPositionOnBox(index);
    }
    
    public int SelectRandomArea()
    {
        System.Random random = new System.Random();
        return random.Next(0, spawningAreas.Count);
    }
    
    Vector2 SelectRandomPositionOnBox(int index)
    {
        Bounds bounds = spawningAreas[index].bounds;
        Vector2 randomPoint = Vector2.zero;
        bool pointFound = false;
        
        randomPoint = new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
        
        return randomPoint;
    }
    
    
}
