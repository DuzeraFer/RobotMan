using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointClick : MonoBehaviour
{
    public Transform player;
    public static bool dir = true;
    public static Vector2 touchPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Robot").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Player.move)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 t = Camera.main.ScreenToViewportPoint(touch.position);

            if (t.y > 0.2)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                        if (touchPosition.x > player.position.x)
                        {
                            dir = true;
                        }
                        else
                        {
                            dir = false;
                        }
                        Movimento(dir);
                        break;
                }
            }
        }       
    }

    public static void Movimento(bool dir)
    {
        if (dir)
        {
            Player.horizontalInput = Player.speed;
        }
        else
        {
            Player.horizontalInput = -Player.speed;
        }
    }
}
