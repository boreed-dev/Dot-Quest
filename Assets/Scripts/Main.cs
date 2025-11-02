using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public enum gamestate
    {
        menu,
        game,
        highScore,
        play,
        dead,
        gameOver,
        nextLevel
    }
    public gamestate state;

    public static Main main;

    public float timer;
    public int point;
    public int pointForLife;
    public int life;
    public int level;
    public int itemIndex;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        timer = 0;
        state = gamestate.menu;

        life = 3;
        point = 0;
        pointForLife = 0;
        level = 1;
        itemIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 10)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if (itemIndex > 7)
            itemIndex = 0;
    }
}
