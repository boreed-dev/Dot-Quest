using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
    public TMP_Text pointText;
    public GameObject lifeSprites;
    public GameObject newlife;

    private int life;
    
    // Start is called before the first frame update
    void Start()
    {
        life = Main.main.life;

        for (int i = 0; i < Main.main.life; i++)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game"));
            if (i  > 0)
            {  
                Transform lifesprite = lifeSprites.transform.GetChild(lifeSprites.transform.childCount - 1);
                Vector3 newPos = new Vector3(lifesprite.position.x + 50f, lifesprite.position.y, lifesprite.position.z);
                Instantiate(newlife, newPos, lifesprite.rotation, lifeSprites.transform);
            }
            else
            {
                Instantiate(newlife, lifeSprites.transform);
            }
            
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        pointText.text = Main.main.point.ToString();

        if (life != Main.main.life)
        {
            if (life > Main.main.life)
            {
                Destroy(lifeSprites.transform.GetChild(lifeSprites.transform.childCount - 1).gameObject);
            }
            else if (life < Main.main.life)
            {
                Transform lifesprite = lifeSprites.transform.GetChild(lifeSprites.transform.childCount - 1);
                Vector3 newPos = new Vector3(lifesprite.position.x + 50f, lifesprite.position.y, lifesprite.position.z);
                                            
                Instantiate(newlife, newPos, lifesprite.rotation, lifeSprites.transform);
            }
        }

        life = Main.main.life;
        
    }
}
