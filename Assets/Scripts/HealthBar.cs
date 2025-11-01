using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Template Heart")]
    public Image heartTemplate;

    private readonly List<Image> active = new();
    private int lastHp= -1;

    void Awake()
    {
        if (!heartTemplate) heartTemplate = GetComponentInChildren<Image>(true);
        heartTemplate.gameObject.SetActive(false);
    }

    void Update()
    {
        if (GameController.instance == null) return;

        int s = GameController.instance.GetPlayerHp();
        if (s != lastHp) { Render(s); lastHp = s; }
    }

    void Render(int value)
    {
        while (active.Count < value)
        {
            var img = Instantiate(heartTemplate, heartTemplate.transform.parent);
            img.gameObject.SetActive(true);
            active.Add(img);
        }

        while (active.Count > value)
        {
            var last = active[^1];
            active.RemoveAt(active.Count - 1);
            Destroy(last.gameObject);
        }
    }
}
