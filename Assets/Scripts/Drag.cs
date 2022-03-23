using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    public bool move;
    private Vector2 posStart;
    private Vector2 pos;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                posStart = transform.position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                if (GetComponent<Collider2D>().OverlapPoint(touchPosition))
                {
                    transform.position = touchPosition;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (move)
                {
                    transform.position = pos;
                }
                else
                {
                    transform.position = posStart;
                }
            }
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "menu")
        {
            pos = collision.transform.position;
            move = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "menu")
        {
            move = false;
        }
    }
}
