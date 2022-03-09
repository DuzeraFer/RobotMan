using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool dir;
    public Player player;
    SpriteRenderer sprite;
    private float y;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        dir = !player.sprite.flipX;
        sprite = GetComponent<SpriteRenderer>();
        sprite.flipX = !dir;
        y = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(dir)
        {
            transform.position = new Vector2(transform.position.x + .25f, y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - .25f, y);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
