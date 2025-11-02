using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    RaycastHit2D hit;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    Animator myAnim;
    Vector3 initialPos;

    private float velocity;
    private float distance;

    public bool inReturn;
    public float timer;

    enum directions
    {
        right,
        left,
        up,
        down
    }
    directions direction;
    
    enum lifeState
    {
        alive,
        dead
    }
    lifeState lState;

    public enum protectionState
    {
        vulnerable,
        invulnerable
    }
    public protectionState pState;

    // Start is called before the first frame update
    void Start()
    {
        pState = protectionState.invulnerable;
        direction = (directions)Random.Range(0, 2);
        velocity = 0.015f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        distance = (spriteRenderer.size.x / 2) + 0.17f;
        inReturn = false;
        initialPos = transform.position;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Main.main.state)
        {
            case Main.gamestate.play:
                switch (lState)
                {
                    case lifeState.alive:
                        switch (direction)
                        {
                            case directions.right:
                                transform.Translate(new Vector3(velocity, 0));
                                hit = Physics2D.Raycast(transform.position, Vector2.right, distance, LayerMask.GetMask("Maze"));
                                if (pState == protectionState.invulnerable)
                                    myAnim.Play("Idle");
                                break;

                            case directions.left:
                                transform.Translate(new Vector3(-velocity, 0));
                                hit = Physics2D.Raycast(transform.position, Vector2.left, distance, LayerMask.GetMask("Maze"));
                                if (pState == protectionState.invulnerable)
                                    myAnim.Play("Left");
                                break;

                            case directions.up:
                                transform.Translate(new Vector3(0, velocity));
                                hit = Physics2D.Raycast(transform.position, Vector2.up, distance, LayerMask.GetMask("Maze"));
                                if (pState == protectionState.invulnerable)
                                    myAnim.Play("Up");
                                break;

                            case directions.down:
                                transform.Translate(new Vector3(0, -velocity));
                                hit = Physics2D.Raycast(transform.position, Vector2.down, distance, LayerMask.GetMask("Maze"));
                                if (pState == protectionState.invulnerable)
                                    myAnim.Play("Bottom");
                                break;
                        }

                        //check raycast collision
                        if (hit)
                            raycastRouting();

                        //10 second timer for vulnerability
                        if (timer > 0)
                        {
                            if (pState == protectionState.vulnerable)
                                myAnim.Play("Vulnerable");
                            else
                                pState = protectionState.vulnerable;
                            countdown();
                        }

                        break;

                    case lifeState.dead:
                        moveToBox();
                        break;
                }
                break;

            case Main.gamestate.dead:
                break;
        }
    }

        

    void countdown()
    {
        timer -= Time.deltaTime;
            
        if (timer <= 0)
        {
            timer = 0;
            pState = protectionState.invulnerable;
        }
            
    }

    void raycastRouting()
    {
        directions previousDir;
        previousDir = direction;

        do
        {
            direction = (directions)Random.Range(0, 4);
        }
        while (previousDir == direction);
    }

    void returnToBox()
    {
        myAnim.Play("Eye");
        lState = lifeState.dead;
    }

    void moveToBox()
    {
        float diffX = 0;
        float diffY = 0;
        float diffZ = 0;

        diffX = transform.position.x - initialPos.x;
        diffY = transform.position.y - initialPos.y;
        diffZ = transform.position.z - initialPos.z;

        boxCollider.enabled = false;
        if (transform.position.x < initialPos.x)
            transform.Translate(new Vector3(velocity, 0));
        else
            transform.Translate(new Vector3(-velocity, 0));

        if (transform.position.y < initialPos.y)
            transform.Translate(new Vector3(0, velocity));
        else
            transform.Translate(new Vector3(0, -velocity));

        if ((diffX < 0.2 && diffX > -0.2) && (diffY < 0.2 && diffY > -0.2) && (diffZ < 0.2 && diffZ > -0.2))
        {
            lState = lifeState.alive;
            pState = protectionState.invulnerable;
            boxCollider.enabled = true;
            myAnim.Play("Idle");
            timer = 0;
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            if (pState == protectionState.vulnerable)
                returnToBox();
    }

}
