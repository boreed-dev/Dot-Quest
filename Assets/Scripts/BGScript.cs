using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BGScript : MonoBehaviour
{
    BoxCollider2D box;
    [SerializeField] float timer;
    private float velocity;
    private bool isOut;

    // Start is called before the first frame update
    void Start()
    {
        velocity = 0.015f;
        isOut = false;
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Main.main.state)
        {
            case Main.gamestate.play:
                if (!this.GetComponent<Ghost>())
                {
                    if (timer < 0)
                    {
                        moveOut();
                        if (isOut)
                        {
                            this.AddComponent<Ghost>();
                            box.enabled = true;
                        }

                    }

                    timer -= Time.deltaTime;
                }
                break;
        }
    }

    void moveOut()
    {
        if (transform.position.x < 0)
        {
            transform.Translate(new Vector2(velocity, 0));
        }
        else if (transform.position.x > 0.1)
        {
            transform.Translate(new Vector2(-velocity, 0));
        }
        else if (transform.position.y < 1.29)
        {
            transform.Translate(new Vector2(0, velocity));
        }
        else { isOut = true; }
    }
}
