using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public ParticleSystem particle;
    public ParticleSystem.MainModule m;
    public float timeC;
    public Camera cam;
    public float speed;
    public Rigidbody2D rb2d;
    float horizontalInput;
    public bool grounded;
    public SpriteRenderer sprite;
    public GameObject[] shoots;
    public Transform startR;
    public Transform startL;
    public Animator anima;
    public bool right;

    public static int coins;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        sprite = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();
        particle = GameObject.FindObjectOfType<ParticleSystem>().GetComponent<ParticleSystem>();
        particle.gameObject.SetActive(false);
        m = particle.main;
        m.startColor = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            particle.gameObject.SetActive(true);
            timeC += Time.deltaTime;
            if (timeC >= 2)
            {
                m.startColor = Color.red;
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            particle.gameObject.SetActive(false);
            Shoot();
            anima.SetTrigger("shoot");
            timeC = 0;
            m.startColor = Color.green;
        }
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        horizontalInput = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(horizontalInput * speed, rb2d.velocity.y);
        if (horizontalInput > 0.01f)
        {
            anima.SetFloat("run", Mathf.Abs(horizontalInput));
            sprite.flipX = false;
        }
        else if (horizontalInput < -0.01f)
        {
            anima.SetFloat("run", Mathf.Abs(horizontalInput));
            sprite.flipX = true;
        }
        else
        {
            horizontalInput = 0;
            anima.SetFloat("run", Mathf.Abs(horizontalInput));
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }
    }
    void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, speed);
        grounded = false;
        anima.SetBool("jump", true);
        anima.SetBool("ground", false);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
            anima.SetBool("jump", false);
            anima.SetBool("ground", true);
        }
        if (collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "death")
        {
            SceneManager.LoadScene(0);
        }
    }

    void Shoot()
    {
        if (!sprite.flipX)
        {
            if (timeC < 1)
                Instantiate(shoots[0], startR);
            else if (timeC < 2)
                Instantiate(shoots[1], startR);
            else
                Instantiate(shoots[2], startR);
        }
        else
        {
            if (timeC < 1)
                Instantiate(shoots[0], startL);
            else if (timeC < 2)
                Instantiate(shoots[1], startL);
            else
                Instantiate(shoots[2], startL);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
            coins++;
        }
    }
}
