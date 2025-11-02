using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTransition : MonoBehaviour
{
    public TMP_Text textTimer;
    public GameObject coins;
    private Animator myAnim;

    public bool animationTerminated;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        animationTerminated = false;
        myAnim = GetComponent<Animator>();
        timer = 5;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (Main.main.state)
        {
            case Main.gamestate.game:
                {
                    if (timer > 0)
                    {
                        timer -= Time.deltaTime;
                        int time = (int)timer;
                        textTimer.text = time.ToString();
                    }
                    else
                    {
                        Main.main.state = Main.gamestate.play;
                        textTimer.text = "";
                    }
                }
                break;
            case Main.gamestate.play:
                {
                    if (coins.transform.childCount == 0)
                    {
                        if (!(myAnim.GetCurrentAnimatorStateInfo(0).IsName("FadeOut")))
                            myAnim.Play("FadeOut");
                    }

                    if (animationTerminated)
                    {
                        Main.main.level += 1;
                        Main.main.state = Main.gamestate.nextLevel;
                    }

                }
                break;
        }
    }
}
