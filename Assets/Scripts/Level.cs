using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class Level : MonoBehaviour
{
    int MaxLevel, LevelNo, i;
    public Button[] LevelBtns;
    public Sprite[] tick;
    public GameObject PuzzleData, PuzzleHolder;

    Play play; 

    public void Start()
    {
        play = FindObjectOfType<Play>();

        StartCoroutine(GetPuzzles());

        LevelNo = PlayerPrefs.GetInt("LevelNo", 1);
        MaxLevel = PlayerPrefs.GetInt("MaxLevel");

        for (i = 0; i <= MaxLevel; i++)
        {
            LevelBtns[i].interactable = true;
            LevelBtns[i].GetComponentInChildren<Text>().text = (i + 1).ToString();

            if (i == (MaxLevel))
            {
                LevelBtns[i].GetComponent<Image>().sprite = null;
                LevelBtns[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
            }

            else
            {
                if (PlayerPrefs.HasKey("skip_" + (i + 1)))
                {
                    LevelBtns[i].GetComponent<Image>().sprite = null;
                    LevelBtns[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
                }
                else
                {
                    LevelBtns[i].GetComponent<Image>().sprite = tick[i];
                }
            }
        }
    }

    IEnumerator GetPuzzles()
    {
        WWW game_database = new WWW("http://pictoword.atwebpages.com/API_pictoword.php");
        yield return game_database;

        var jsonArr = JSON.Parse(game_database.text);

        for (int i = 0; i < jsonArr.Count; i++)
        {
            int LevelNo = i+1;
            string TrueAns = jsonArr[i]["word"].Value;
            string input = jsonArr[i]["input"].Value;
            string HintAns = jsonArr[i]["hint"].Value;
            string picture = jsonArr[i]["picture"].Value;

            WWW image_data = new WWW("http://f28-preview.awardspace.net/pictoword.atwebpages.com/PictoWord_images/" + picture);
            yield return image_data;

            LevelBtns[i].GetComponent<Button>().onClick.AddListener(() => LevelBtnClick(LevelNo));

        }

    }

    public void LevelBtnClick(int no)
    {
        PlayerPrefs.SetInt("LevelNo", no);

        SceneManager.LoadScene("Play");

    }

    public void BackBtn()
    {
        SceneManager.LoadScene("Home");
    }
}
