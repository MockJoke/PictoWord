using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; 

public class Database : MonoBehaviour
{
    public GameObject prefab, holder;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetPuzzles()); 
    }

    IEnumerator GetPuzzles()
    {
        WWW game_database = new WWW("http://pictoword.atwebpages.com/API_pictoword.php");
        yield return game_database;

        var jsonArr = JSON.Parse(game_database.text); 

        for(int i=0; i<jsonArr.Count; i++)
        {
            int LevelNo = int.Parse(jsonArr[i]["LevelNo"].Value);
            string word = jsonArr[i]["word"].Value;
            string input = jsonArr[i]["input"].Value;
            string HintAns = jsonArr[i]["HintAns"].Value;
            string picture = jsonArr[i]["picture"].Value;

            WWW image_data = new WWW("http://localhost/Basic/PictoWord_images/"+picture);
            yield return image_data;

            GameObject data = Instantiate(prefab, holder.transform);
            data.transform.GetChild(1).GetComponent<Text>().text = word;
            data.transform.GetChild(0).GetComponent<RawImage>().texture = image_data.texture;
            data.GetComponent<Button>().onClick.AddListener(() => on_PuzzleClick(LevelNo));

            print(picture);

        }
    }

    public void on_PuzzleClick(int Puzzle_no)
    {
        StartCoroutine(Get_SinglePuzzle(Puzzle_no));
    }

    IEnumerator Get_SinglePuzzle(int P_no)
    {
        WWW puzzle_data = new WWW("http://pictoword.atwebpages.com/Get_SinglePuzzle.php" + P_no);
        yield return puzzle_data;

        var jsonArr = JSON.Parse(puzzle_data.text);
        print(jsonArr["word"].Value);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
