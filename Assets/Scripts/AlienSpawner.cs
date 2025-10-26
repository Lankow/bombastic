using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject alienPrefab;
    public Camera cam;

    [Header("Spawn Settings")]
    public float groundY = -2.5f;
    public float edgePadding = 0.5f;
    public float minSpawnInterval = 0.4f;
    public float maxSpawnInterval = 1.6f;
    public AnimationCurve difficultyCurve = AnimationCurve.EaseInOut(0, 0, 60, 1);

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void CacheEdgePositions()
    {
    }

    void SpawnAlien()
    {
    }

    }
