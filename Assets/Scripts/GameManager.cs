using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int score = 0;

    public GameObject alien;
    public List<Transform> spawnPoints;

    public float spawnRate;

    private const float MinVerticalPos = -1.85f;
    private const float MaxVerticalPos = -0.8f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameStart();
    }

    void Update()
    {
        
    }

    public void ScoreUp()
    {
        score++;
        print(score);
    }

    void SpawnAlien()
    {
        var randIndex = Random.Range(0, spawnPoints.Count);
        var spawnPosition = spawnPoints[randIndex].position;

        spawnPosition.y = Random.Range(MinVerticalPos, MaxVerticalPos);

        Instantiate(alien, spawnPosition, Quaternion.identity);
    }

    public void GameStart()
    {
        InvokeRepeating("SpawnAlien", 1f, spawnRate);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
