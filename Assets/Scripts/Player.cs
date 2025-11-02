using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PacMan : MonoBehaviour
{
    private Animator myAnim;
    private CircleCollider2D myCollider;
    private AudioSource []myAudioSource;

    [SerializeField] private AudioClip eat;
    [SerializeField] private AudioClip deadEffect;
    [SerializeField] private AudioClip eatFruit;
    [SerializeField] private AudioClip eatGhost;
    [SerializeField] private AudioClip extraLife;
    [SerializeField] private AudioClip invulenarbleEffect;

    Vector3 initialPos;
    public bool deadAnimationTerminated;
    private float timer;
    private int eatenGhost;

    enum lifeState
    {
        vulnerable,
        invulnerable
    }
    lifeState state;
        
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<CircleCollider2D>();
        myAudioSource = GetComponents<AudioSource>();

        eatenGhost = 0;
        initialPos = transform.position;
        deadAnimationTerminated = false;
        state = lifeState.vulnerable;
        myCollider.enabled = true;

        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (Main.main.state)
        {
            case Main.gamestate.play:
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    if (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("IdleUp"))
                        myAnim.Play("IdleUp");

                    transform.Translate(new Vector2(0, 0.015f));
                }

                else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    if (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("IdleBot"))
                        myAnim.Play("IdleBot");

                    transform.Translate(new Vector2(0, -0.015f));
                }

                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    if (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                        myAnim.Play("Idle");

                    transform.Translate(new Vector2(0.015f, 0));
                }

                else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    if (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("IdleSx"))
                        myAnim.Play("IdleSx");

                    transform.Translate(new Vector2(-0.015f, 0));
                }

                if (timer > 0)
                    countdown();

                if (Main.main.pointForLife > 10000)
                {
                    Main.main.life++;
                    Main.main.pointForLife = 0;
                    myAudioSource[2].clip = extraLife;
                    myAudioSource[2].Play();
                }

                break;
            case Main.gamestate.dead:
                dead();
                break;
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "RightTeleporter":
                transform.position = new Vector3(-4.7f, 0.28f, -0.1f);
                break;
            case "LeftTeleporter":
                transform.position = new Vector3(5f, 0.28f, -0.1f);
                break;
            case "Coin":
                Destroy(collision.gameObject);
                Main.main.point += 10;
                Main.main.pointForLife += 10;
                myAudioSource[0].clip = eat;
                if (!myAudioSource[0].isPlaying)
                    myAudioSource[0].Play();
                break;
            case "PowerPills":
                Destroy(collision.gameObject);
                state = lifeState.invulnerable;
                var gList = FindObjectsByType<Ghost>(FindObjectsSortMode.None);
                foreach (Ghost gObject in gList)
                    gObject.timer = 10f;
                timer = 10f;
                Main.main.point += 50;
                Main.main.pointForLife += 50;
                myAudioSource[1].clip = invulenarbleEffect;
                myAudioSource[1].loop = true;
                myAudioSource[1].Play();
                break;
            case "Ghost":
                if (state == lifeState.vulnerable)
                {
                    Main.main.state = Main.gamestate.dead;
                    myAnim.Play("Died");
                    myCollider.enabled = false;
                    Main.main.life--;
                    myAudioSource[1].Stop();
                    myAudioSource[2].clip = deadEffect;
                    myAudioSource[2].Play();
                    
                }
                else
                {
                    var gList2 = FindObjectsByType<Ghost>(FindObjectsSortMode.None);
                    foreach (Ghost gObject in gList2)
                    {
                        if (collision.gameObject.name == gObject.name)
                        {
                            if (gObject.pState == Ghost.protectionState.invulnerable)
                            {
                                Main.main.state = Main.gamestate.dead;
                                myAnim.Play("Died");
                                myCollider.enabled = false;
                                Main.main.life--;
                                myAudioSource[1].Stop();
                                myAudioSource[2].clip = deadEffect;
                                myAudioSource[2].Play();
                                
                            }
                            else
                            {
                                eatenGhost++;
                                switch (eatenGhost)
                                {
                                    case 1:
                                        {
                                            Main.main.point += 200;
                                            Main.main.pointForLife += 200;
                                        }
                                        break;
                                    case 2:
                                        {
                                            Main.main.point += 400;
                                            Main.main.pointForLife += 400;
                                        }
                                        break;
                                    case 3:
                                        {
                                            Main.main.point += 800;
                                            Main.main.pointForLife += 800;
                                        }
                                        break;
                                    case 4:
                                        {
                                            Main.main.point += 1600;
                                            Main.main.pointForLife += 1600;
                                        }
                                        break;

                                }
                            }
                        }
                    }
                    myAudioSource[2].clip = eatGhost;
                    myAudioSource[2].Play();
                }
                break;
            case "Item":
                Destroy(collision.gameObject);
                myAudioSource[2].clip = eatFruit;
                myAudioSource[2].Play();
                switch (Main.main.itemIndex)
                {
                    case 0:
                        Main.main.point += 100;
                        Main.main.itemIndex++;
                        break;
                    case 1:
                        Main.main.point += 300;
                        Main.main.itemIndex++;
                        break;
                    case 2:
                        Main.main.point += 500;
                        Main.main.itemIndex++;
                        break;
                    case 3:
                        Main.main.point += 700;
                        Main.main.itemIndex++;
                        break;
                    case 4:
                        Main.main.point += 1000;
                        Main.main.itemIndex++;
                        break;
                    case 5:
                        Main.main.point += 2000;
                        Main.main.itemIndex++;
                        break;
                    case 6:
                        Main.main.point += 3000;
                        Main.main.itemIndex++;
                        break;
                    case 7:
                        Main.main.point += 5000;
                        Main.main.itemIndex++;
                        break;
                }
                break;
        }
            
    }

    void dead()
    {
        if (deadAnimationTerminated)
        {
            if (Main.main.life == 0)
            {
                Main.main.state = Main.gamestate.gameOver;
            }
            else
            {
                resetPosition();
            }
        } 
    }

    void resetPosition()
    {
        transform.position = initialPos;
        myAnim.Play("Idle");
        myCollider.enabled = true;
        deadAnimationTerminated = false;
        Main.main.state = Main.gamestate.play;
    }

    void countdown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = 0;
            state = lifeState.vulnerable;
            eatenGhost = 0;
            myAudioSource[1].loop = false;
            myAudioSource[1].Stop();
        }
    }

}
