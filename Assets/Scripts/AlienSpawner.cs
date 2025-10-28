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

    float elapsed;
    float nextSpawnAt;

    Vector3 leftPos;
    Vector3 rightPos;

    void Start()
    {
        if (!cam) cam = Camera.main;
        CacheEdgePositions();
        ScheduleNextSpawn();
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if (Time.time >= nextSpawnAt)
        {
            SpawnAlien();
            ScheduleNextSpawn();
        }
    }

    void CacheEdgePositions()
    {
        Vector3 leftWorld = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, -cam.transform.position.z));
        Vector3 rightWorld = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, -cam.transform.position.z));

        leftPos = new Vector3(leftWorld.x - edgePadding, groundY, 0);
        rightPos = new Vector3(rightWorld.x + edgePadding, groundY, 0);
    }

    void ScheduleNextSpawn()
    {
        float t = Mathf.Clamp01(difficultyCurve.Evaluate(elapsed));
        float interval = Mathf.Lerp(maxSpawnInterval, minSpawnInterval, t);
        nextSpawnAt = Time.time + interval;
    }

    void SpawnAlien()
    {
        bool fromLeft = Random.value < 0.5f;
        Vector3 pos = fromLeft ? leftPos : rightPos;

        GameObject go = Instantiate(alienPrefab, pos, Quaternion.identity);
        Alien a = go.GetComponent<Alien>();
        a.Init(GameController.instance, fromLeft ? Vector2.right : Vector2.left, GameController.instance.centerX);
    }
}
