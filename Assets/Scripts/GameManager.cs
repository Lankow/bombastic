using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int score = 0;

    public GameObject alien;
    public GameObject menuUI;
    public List<Transform> spawnPoints;
    public TextMeshProUGUI scoreText;

    public float spawnRate;

    private const float MinVerticalPos = -1.85f;
    private const float MaxVerticalPos = -0.8f;

    bool gameStarted = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !gameStarted)
        {
            gameStarted = true;
            GameStart();
        }
    }

    public void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
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
        menuUI.SetActive(false);
        scoreText.gameObject.SetActive(true);
        InvokeRepeating("SpawnAlien", 1f, spawnRate);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
