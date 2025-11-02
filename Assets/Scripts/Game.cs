using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject coins;
    public GameObject items;
    private GameObject copyItems;
    private SpriteRenderer itemSprite;
    private AudioSource effect;

    [SerializeField] private AudioClip start;
    
    int coinCount;
    int spawnNumber;

    private float deleteTimer;
    private bool itemSpawned;

    // Start is called before the first frame update
    void Start()
    {
        itemSprite = items.GetComponent<SpriteRenderer>();
        effect = GetComponent<AudioSource>();
        spawnNumber = 2;
        deleteTimer = 5;
        coinCount = coins.transform.childCount;
        Main.main.state = Main.gamestate.game;
        effect.clip = start;
        effect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Main.main.state)
        {
            case Main.gamestate.game:
                {
                   
                }
                break;
            case Main.gamestate.play:
                {
                    spawnItems();
                    deleteItems();

                }break;
        }

    }

    private void spawnItems()
    {

        //spawn when the coins are /2 and /4 
        if (spawnNumber > 0)
        {
            if ((coinCount / 2) > coins.transform.childCount)
            {
                loadItems();
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
                copyItems = Instantiate(items, this.transform.parent);
                coinCount = coins.transform.childCount;
                spawnNumber--;
                itemSpawned = true;
            }
        }

#if false
        if (coins.transform.childCount < 180)
        {
            loadItems();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
            copyItems = Instantiate(items, this.transform.parent);
        }
#endif
    }

    private void deleteItems()
    {
        if (itemSpawned)
        {
            if (deleteTimer > 0)
            {
                deleteTimer -= Time.deltaTime;
            }
            else
            {
                if (GameObject.Find("Items(Clone)") != null)
                {
                    Destroy(copyItems);
                }
                itemSpawned = false;
                deleteTimer = 5;
            }
        } 
    }

    private void loadItems()
    {
        switch (Main.main.itemIndex)
        {
            case 0:
                {
                    itemSprite.sprite = Resources.Load<Sprite>("B-1");
                }
                break;
            case 1:
                {
                    itemSprite.sprite = Resources.Load<Sprite>("B-2");
                }break;
            case 2:
                {
                    itemSprite.sprite = Resources.Load<Sprite>("B-3");
                }
                break;
            case 3:
                {
                    itemSprite.sprite = Resources.Load<Sprite>("B-4");
                }
                break;
            case 4:
                {
                    itemSprite.sprite = Resources.Load<Sprite>("B-5");
                }
                break;
            case 5:
                {
                    itemSprite.sprite = Resources.Load<Sprite>("B-6");
                }
                break;
            case 6:
                {
                    itemSprite.sprite = Resources.Load<Sprite>("B-7");
                }
                break;
            case 7:
                {
                    itemSprite.sprite = Resources.Load<Sprite>("B-8");
                }
                break;
        }
    }
}
