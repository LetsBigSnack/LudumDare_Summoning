using UnityEngine;
using UnityEngine.Tilemaps;

public class CompressBounds : MonoBehaviour
{
    public Tilemap tilemap;

    void Start()
    {
        tilemap.CompressBounds();
    }
}

