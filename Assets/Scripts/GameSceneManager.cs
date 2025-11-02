using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private UnityEngine.SceneManagement.Scene load;
    private UnityEngine.SceneManagement.Scene unload;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        switch (Main.main.state)
        {
            case Main.gamestate.menu:
                {
                    load = SceneManager.GetSceneByName("Menu");
                    if (load.name == null)
                    {
                        unload = SceneManager.GetSceneByName("GameOver");
                        if(!(unload.name == null))
                        {
                            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("GameOver"));
                            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Game"));
                        }
                        unload = SceneManager.GetSceneByName("HighScore");
                        if(!(unload.name == null))
                        {
                            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("HighScore"));
                        }

                        SceneManager.LoadScene(1, LoadSceneMode.Additive);
                    }
                }break;
            case Main.gamestate.game:
                {
                    load = SceneManager.GetSceneByName("Game");
                    if(load.name == null)
                    {
                        SceneManager.LoadScene(2, LoadSceneMode.Additive);
                        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Menu"));
                    }
                }
                break;
            case Main.gamestate.highScore:
                {
                    load = SceneManager.GetSceneByName("HighScore");
                    if (load.name == null)
                    {
                        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Menu"));
                        SceneManager.LoadScene(4, LoadSceneMode.Additive);
                    }
                }
                break;
            case Main.gamestate.gameOver:
                {
                    load = SceneManager.GetSceneByName("GameOver");
                    if(load.name == null)
                    {
                        SceneManager.LoadScene(3, LoadSceneMode.Additive);
                    }
                }break;
            case Main.gamestate.nextLevel:
                {
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Game"));
                    SceneManager.LoadScene(2, LoadSceneMode.Additive);
                }break;
        }
    }
}
