using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [Header("Digits 0-9")]
    public Sprite[] numberSprites;

    [Header("Template Image (Digit_0)")]
    public Image digitTemplate;

    private readonly List<Image> active = new();
    private int lastScore = -1;

    void Awake()
    {
        if (!digitTemplate) digitTemplate = GetComponentInChildren<Image>(true);
        digitTemplate.gameObject.SetActive(false);
    }

    void Update()
    {
        if(GameController.instance == null) return;

        int s = GameController.instance.GetScore();
        if (s != lastScore) { Render(s); lastScore = s; }
    }

    void Render(int value)
    {
        string txt = value.ToString();

        while (active.Count < txt.Length)
        {
            var img = Instantiate(digitTemplate, digitTemplate.transform.parent);
            img.gameObject.SetActive(true);
            active.Add(img);
        }

        for (int i = 0; i < active.Count; i++)
            active[i].gameObject.SetActive(i < txt.Length);

        for (int i = 0; i < txt.Length; i++)
        {
            int d = txt[i] - '0';
            active[i].sprite = numberSprites[d];
        }
    }
}