using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject alien;
    public List<Transform> spawnPoints;

    public float spawnRate;

    private const float MinVerticalPos = -0.24f;
    private const float MaxVerticalPos = 0.05f;
    private int score = 0;

    public int playerHP = 3;

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

    public int GetScore()
    {
        return score;
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
