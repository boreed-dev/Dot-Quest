using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HighScore : MonoBehaviour
{
    [SerializeField] private GameData score = new GameData();

    string path;
    string filename = "savedata";

    public TMP_Text Time_1;
    public TMP_Text Time_2;
    public TMP_Text Time_3;
    public TMP_Text Time_4;
    public TMP_Text Time_5;

    public TMP_Text Name_1;
    public TMP_Text Name_2;
    public TMP_Text Name_3;
    public TMP_Text Name_4;
    public TMP_Text Name_5;

    private Animator myAnim;
    public bool animationTerminated;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        animationTerminated = false;
        path = Application.dataPath + "/";
        load();
        WriteInScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTerminated)
        {
            Main.main.state = Main.gamestate.menu;
        }
    }

    public void back()
    {
        myAnim.Play("FadeOut");
    }

    void load()
    {
        int count = 0;
        string fullPath = Path.Combine(path, filename);
        if (File.Exists(fullPath))
        {
            //load the serialized data from file
            string dataToLoad = "";
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            if (dataToLoad != "")
            {
                foreach (char c in dataToLoad)
                    if (c == '}' || c == '{')
                        count++;

                if (count % 2 != 0)
                    dataToLoad = dataToLoad.Remove(dataToLoad.Length - 1);

                //convert the serialized data into unity object
                //GameData gamedata = JsonUtility.FromJson<GameData>(dataToLoad);
                GameData gamedata = JsonUtility.FromJson<GameData>(dataToLoad);
                score = gamedata;
            }
        }
        else
            Debug.Log("The file don't exsist");
    }

    void WriteInScene()
    {
        for (int i = 0; i < score.playerInfo.Count; i++)
        {
            switch (i)
            {
                case 0:
                    Name_1.text = score.playerInfo[i].value.ToString();
                    Time_1.text = score.playerInfo[i].text;
                    break;
                case 1:
                    Name_2.text = score.playerInfo[i].value.ToString();
                    Time_2.text = score.playerInfo[i].text;
                    break;
                case 2:
                    Name_3.text = score.playerInfo[i].value.ToString();
                    Time_3.text = score.playerInfo[i].text;
                    break;
                case 3:
                    Name_4.text = score.playerInfo[i].value.ToString();
                    Time_4.text = score.playerInfo[i].text;
                    break;
                case 4:
                    Name_5.text = score.playerInfo[i].value.ToString();
                    Time_5.text = score.playerInfo[i].text;
                    break;
            }
        }
    }
}
