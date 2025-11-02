using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

[System.Serializable]
public class scoringStruct
{
    public string text;
    public int value;
}

//game data class
[System.Serializable]
public class GameData
{
    public List<scoringStruct> playerInfo;
}

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameData score = new GameData();
    public scoringStruct valueContainer;

    string path;

    string filename = "savedata";

    public TMP_InputField inputField;
    private Animator myAnim;
    public bool animationTerminated;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        animationTerminated = false;
        path =  = Application.dataPath + "/";
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTerminated)
        {
            Main.main.state = Main.gamestate.menu;
        }
    }

    public void saveAndReturn()
    {
        //save Game
        saveToFile();
        //return to MainMenu
        myAnim.Play("FadeOut");
    }

    private void saveToFile()
    {
        //load the value from the file into a list/array
        Load();

        //add point and name
        valueContainer.text = inputField.text;
        valueContainer.value = Main.main.point;
        score.playerInfo.Add(valueContainer);

        //sort the list - 
        //score.playerInfo.Reverse();
        //score.playerInfo = score.playerInfo.OrderByDescending(x => score.playerInfo.).ToList();
        sortingList();

        //if score.lenght > 5, erase the last element
        if (score.playerInfo.Count > 5)
            score.playerInfo.RemoveAt(5);

        //save data into file
        Save();

        //resetAll(); necessary ??
    }

    private void Load()
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

    void Save()
    {
        //gameData.score = score;
        string fullPath = Path.Combine(path, filename);
        //string dataToStore = JsonUtility.ToJson(gameData, true);
        string dataToStore = JsonUtility.ToJson(score);
        using (FileStream stream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
    }

    void sortingList()
    {
        int listIndex = 0;
        scoringStruct temp;
        for (int i = 0; i < score.playerInfo.Count; i++)
        {
            int max = 0;
            listIndex = i;
            for (int j = i; j < score.playerInfo.Count; j++)
            {
                if (score.playerInfo[j].value > max)
                {
                    max = score.playerInfo[j].value;
                    listIndex = j;
                }

            }
            temp = score.playerInfo[i];
            score.playerInfo[i] = score.playerInfo[listIndex];
            score.playerInfo[listIndex] = temp;
        }
    }
}
