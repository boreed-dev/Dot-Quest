using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameEvent : MonoBehaviour
{
    EventSystem myEventSystem;
    // Start is called before the first frame update
    void Start()
    {
        myEventSystem = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Main.main.state)
        {
            case Main.gamestate.gameOver:
                {
                    myEventSystem.enabled = false;
                }break;
        }
    }
}
