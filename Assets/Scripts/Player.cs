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

    public GameObject m_Button;
    ButtonEvent s_Button;

    public bool haveClicked;

    public Joystick m_joystick;

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
        s_Button = m_Button.GetComponent<ButtonEvent>();

        haveClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (s_Button.buttonPressed == true)
        {
            particle.gameObject.SetActive(true);
            timeC += Time.deltaTime;
            if (timeC >= 2)
            {
                m.startColor = Color.red;
            }
            haveClicked = true;
        }
        if (s_Button.buttonPressed == false && haveClicked == true)
        {
            particle.gameObject.SetActive(false);
            Shoot();
            anima.SetTrigger("shoot");
            timeC = 0;
            m.startColor = Color.green;

            haveClicked = false;
        }

        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        //horizontalInput = Input.GetAxis("Horizontal");
        //horizontalInput = m_joystick.Horizontal;
        rb2d.velocity = new Vector2(horizontalInput, rb2d.velocity.y);

        if (m_joystick.Horizontal >= 0.2f)
        {
            horizontalInput = speed;
        }
        else if (m_joystick.Horizontal <= -0.2f)
        {
            horizontalInput = -speed;
        }
        else
        {
            horizontalInput = 0;
        }

        if (horizontalInput > 0.2f)
        {
            anima.SetFloat("run", Mathf.Abs(horizontalInput));
            sprite.flipX = false;
        }
        else if (horizontalInput < -0.2f)
        {
            anima.SetFloat("run", Mathf.Abs(horizontalInput));
            sprite.flipX = true;
        }
        else
        {
            horizontalInput = 0;
            anima.SetFloat("run", Mathf.Abs(horizontalInput));
        }      
    }

    public void Jump()
    {
        if (grounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, speed);
            grounded = false;
            anima.SetBool("jump", true);
            anima.SetBool("ground", false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
            anima.SetBool("jump", false);
            anima.SetBool("ground", true);
        }

        if (collision.gameObject.tag == "death")
        {
            coins = 0;
            SceneManager.LoadScene(2);
        }

        if (collision.gameObject.tag == "door")
        {
            coins = 0;
            SceneManager.LoadScene(3);
        }
    }
}
