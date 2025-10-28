using UnityEngine;

public class Alien : MonoBehaviour
{
    GameController controller;
    Vector2 moveDir;
    public float speed = 2f;
    float centerX;

    public bool FromLeft { get; private set; }

    [Header("Stats")]
    public Vector2 speedRange = new Vector2(1.2f, 2.6f);

    public void Init(GameController controller, Vector2 direction, float centerX)
    {
        this.controller = controller;
        this.moveDir = direction.normalized;
        this.centerX = centerX;
        this.FromLeft = direction.x > 0;
        speed = Random.Range(speedRange.x, speedRange.y);
    }

    void Update()
    {
        var pos = transform.position;
        var target = new Vector3(0f, pos.y, pos.z);

        transform.position = Vector3.MoveTowards(pos, target, speed * Time.deltaTime);
    }



    void Start()
    {
        if(transform.position.x < 0)
        {
            var scale = transform.localScale;
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
    }

    private void OnMouseDown()
    {
        GameController.instance.ScoreUp();
        Destroy(gameObject);
    }
}
