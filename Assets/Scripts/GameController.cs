using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject alien;
    public List<Transform> spawnPoints;

    public float centerX = 0f;
    public float shootCooldown = 0.15f;
    public float spawnRate;

    public int playerHP = 3;

    private float nextShootAt;
    private int score = 0;

    readonly List<Alien> activeAliens = new List<Alien>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Time.time < nextShootAt) return;

        bool clicked = false;
        Vector2 pos = Vector2.zero;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            pos = Input.mousePosition;
        }
#endif

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            clicked = true;
            pos = Input.GetTouch(0).position;
        }

        if (!clicked) return;

        nextShootAt = Time.time + shootCooldown;

        bool leftSide = pos.x < Screen.width / 2f;
        Alien target = GetTargetOnSide(leftSide);

        if (target != null)
        {

            target.Kill();
            ScoreUp();
        }
    }

    Alien GetTargetOnSide(bool leftSide)
    {
        Alien target = null;
        float best = float.MaxValue;

        foreach (var a in activeAliens)
        {
            if (a == null) continue;
            if (leftSide && !a.FromLeft) continue;
            if (!leftSide && a.FromLeft) continue;

            var distance = Mathf.Abs(a.transform.position.x - centerX);

            if (distance < best)
            {

                best = distance;
                target = a;
            }
        }
        return target;
    }

    public void RegisterAlien(Alien a)
    {
        if (!activeAliens.Contains(a))
            activeAliens.Add(a);
    }

    public void UnregisterAlien(Alien a)
    {
        activeAliens.Remove(a);
    }

    public void OnAlienBreach(Alien a)
    {
        UnregisterAlien(a);
        playerHP--;

        if (playerHP <= 0)
            GameOver();
    }

    public void ScoreUp()
    {
        score++;
        Debug.Log("Score: " + score);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public int GetScore() => score;
}
