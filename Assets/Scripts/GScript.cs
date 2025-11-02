using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GScript : MonoBehaviour
{
    public GameObject BG;
    public GameObject RG;
    public GameObject YG;
    public GameObject PG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (Main.main.state)
        {
            case Main.gamestate.dead:
                for (int i = 0; i < transform.childCount; i++)
                    Destroy(transform.GetChild(i).gameObject);
                break;

            case Main.gamestate.play:
                //instantiate ghost if not already present
                if (transform.childCount < 4)
                {
                    bool bg = false;
                    bool rg = false;
                    bool yg = false;
                    bool pg = false;
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        switch (transform.GetChild(i).gameObject.name)
                        {
                            case "BG":
                                bg = true;
                                break;
                            case "RG":
                                rg = true;
                                break;
                            case "YG":
                                yg = true;
                                break;
                            case "PG":
                                pg = true;
                                break;
                        }
                    }

                    if (!bg)
                        Instantiate(BG, this.transform);
                    if (!rg)
                        Instantiate(RG, this.transform);
                    if (!yg)
                        Instantiate(YG, this.transform);
                    if (!pg)
                        Instantiate(PG, this.transform);
                }
                break;
        }
    }
}
