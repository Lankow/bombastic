using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject alien;
    public GameObject captain;

    public List<Transform> spawnPoints;

    public float shootCooldown = 0.15f;
    public float spawnRate;
    public float centerX = 0f;

    public int playerHP = 3;

    private float nextShootAt;
    private int score = 0;

    readonly List<Alien> activeAliens = new List<Alien>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Time.time < nextShootAt) return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            nextShootAt = Time.time + shootCooldown;
            FireOnSide(Input.mousePosition);
            return;
        }
#endif
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            nextShootAt = Time.time + shootCooldown;
            FireOnSide(Input.GetTouch(0).position);
        }
    }

    void FireOnSide(Vector2 screenPos)
    {
        bool leftSide = screenPos.x < Screen.width * 0.5f;
        HandleCaptainFacing(leftSide);

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

    void HandleCaptainFacing(bool clickedLeft)
    {
        if (!captain) return;
        var s = captain.transform.localScale;
        float dir = s.x;
        if ((dir < 0 && !clickedLeft) || (dir > 0 && clickedLeft))
            captain.transform.localScale = new Vector3(-dir, s.y, s.z);
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
        SceneManager.LoadScene("MainMenu");
    }

    public int GetScore() => score;
}
