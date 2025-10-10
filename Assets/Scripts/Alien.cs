using UnityEngine;

public class Alien : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        var pos = transform.position;
        var target = new Vector3(0f, pos.y, pos.z);

        transform.position = Vector3.MoveTowards(pos, target, speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        GameManager.instance.ScoreUp();
        Destroy(gameObject);
    }
}
