using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Animator myAnim;
    public bool transitionTerminated;

    enum menuState
    {
        menu,
        loadGame,
        loadHighScore,

    }

    menuState state;

    // Start is called before the first frame update
    void Start()
    {
        state = menuState.menu;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionTerminated)
        {
            switch (state)
            {
                case menuState.loadGame:
                    {
                        Main.main.state = Main.gamestate.game;
                        Main.main.point = 0;
                        Main.main.life = 3;
                        Main.main.pointForLife = 0;
                        Main.main.level = 1;
                        Main.main.itemIndex = 0;
                    }
                    break;
                case menuState.loadHighScore:
                    {
                        Main.main.state = Main.gamestate.highScore;
                    }
                    break;
            }
        }
    }

    public void loadNewGame()
    {
        state = menuState.loadGame;
        myAnim.Play("FadeOut");
    }

    public void loadHighScore()
    {
        state = menuState.loadHighScore;
        myAnim.Play("FadeOut");
    }
}
