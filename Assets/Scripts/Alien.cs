using UnityEngine;

public class Alien : MonoBehaviour
{
    public Vector2 speedRange = new Vector2(1.2f, 2.6f);
    public bool FromLeft { get; private set; }

    float speed;
    Vector2 moveDir;

    GameController gc;
    public int alienHP;

    public void Init(GameController controller, bool fromLeft)
    {
        gc = controller;
        FromLeft = fromLeft;
        moveDir = fromLeft ? Vector2.right : Vector2.left;
        speed = Random.Range(speedRange.x, speedRange.y);
        alienHP = Random.Range(1, 3);

        gc.RegisterAlien(this);
        if (FromLeft)
        {
            var scale = transform.localScale;
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
    }

    void Update()
    {
        transform.Translate(moveDir * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Alien collided with: " + col.gameObject.name);

        if (col.CompareTag("Captain"))
        {
            gc.OnAlienBreach(this);
            Destroy(gameObject);
        }
    }

    public void HitByProjectile()
    {
        Debug.Log($"Alien hit by projectile. Alien HP: {alienHP}.");
        if (--alienHP <= 0) Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
        gc.ScoreUp();
    }

    void OnDestroy()
    {
        if (gc) gc.UnregisterAlien(this);
    }
}
