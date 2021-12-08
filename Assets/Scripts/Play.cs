using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class Play : MonoBehaviour
{
    public GameObject PlayObj, SuccessObj, FailObj, HintObj, can, PuzzleData, PuzzleHolder;

    public GameObject DynamicBtn, InputBar, OutputBar;

     string TrueAns, input, hint, picture; 

    /* string[] input = {"bqwrtayuilopsdev", "dyueiogpsefhajkl", "dqweyrtubaiooplsa", "gqwiertyojkluzxd", "qwgtyreukaclvish", "whqetnydpejrlson", "qkwauiertklpacvb", "qwlghambvpnkasrd", "wqmtayuhjkrpeioz",
                         "ewmqyaioptklhabn", "rtmqwhbavcpzxpse", "pmoyeksqwscvizxn", "pwqohfgsxbzamnck", "rplaskjhfgoqwrcd", "pgrohqwobvnexyzf", "ytrqwosghcepzxnm", "iusqwalrkjvadhmn", "kjsqwapungchbozv", "posqwieckldvxabz",
                         "qwesrtouinplkjhm", "posqwterdlbimncg", "yrtqwebcvexdzfak", "rwtiqplblimazxns", "mbzqwlaertkjands"};    //generated input btns
    
    string[] TrueAns = { "bale" + "degea", "dybala", "giroud", "grealish", "hendo", "kaka", "lampard", "mahrez", 
                         "mata", "mbappe", "messi", "pogba", "rashford", "rooney", "rose", "salah", "sancho", "silva", 
                         "son", "sterling", "tevez", "willian", "zlatan"};         //true ans 
    
    string[] HintAns = { "_a_e", "_e_e_", "_y__l_", "_i__u_", "g_e___s_", "_e__e___n", "__k_", "__m__r_", "__h_e_",
                         "__t_", "_b__p_", "_e_s_", "__g_a", "r___f__d", "_o_n__", "__s_", "__l__", "s_n___", "_i_v_",
                         "_o_", "s__r_i__", "__v_z", "w__l_a_", "z__t_n"};         //hint ans  */

    string CurAns, UserAns, CurInp;                          //ans of the current going on puzzle 
    //public Sprite[] puzzles;                //images of puzzles to be changed per level 
    public Image PuzzleBoard;               //images to be replaced on lock after a successful submit 
    public Text LevelBoard, ScoreBoard, HintBoard;     //Level&ScoreBoard - to change the displayed LevelNo & score  
    int LevelNo, MaxLevel, score = 0;
    int[] BtnPos = new int[10];             //to get positions of ip to op btn 
    public RawImage puzzle;
    // Start is called before the first frame update
    void Start()
    {
        LevelNo = PlayerPrefs.GetInt("LevelNo", 1);
        on_PuzzleClick(LevelNo); 

        MaxLevel = PlayerPrefs.GetInt("MaxLevel", 0);

        score = PlayerPrefs.GetInt("score", 0);
        ScoreBoard.text = "SCORE : " + score;

        LevelBoard.text = "LEVEL" + LevelNo;

        //float Ipwidth = (InputBar.GetComponent<RectTransform>().rect.width / 8) - 10;
        //float Ipheight = (InputBar.GetComponent<RectTransform>().rect.height / 2) - 10;

        //InputBar.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Ipwidth, Ipheight);

        //float Opwidth = (OutputBar.GetComponent<RectTransform>().rect.width / CurAns.Length) - 100;
        //float Opheight = (OutputBar.GetComponent<RectTransform>().rect.height) - 10;

        //OutputBar.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Opwidth, Opheight);

    }


    void on_PuzzleClick(int LevelNo)
    {
        StartCoroutine(Get_SinglePuzzle(LevelNo));
    }

    IEnumerator Get_SinglePuzzle(int puzzle_no)
    {
        WWW puzzle_data = new WWW("http://pictoword.atwebpages.com/Get_SinglePuzzle.php?ID=" + puzzle_no);
        yield return puzzle_data;

        var jsonArr = JSON.Parse(puzzle_data.text);

        LevelNo = int.Parse(jsonArr["LevelNo"].Value);
        TrueAns = jsonArr["word"].Value;
        input = jsonArr["input"].Value;
        hint = jsonArr["hint"].Value;
        picture = jsonArr["picture"].Value;
        
        CurInp = input;
        CurAns = TrueAns;

        for (int i = 0; i < CurInp.Length; i++)
        {
            GameObject Btn = Instantiate(DynamicBtn, InputBar.transform);       //clones generation as input buttons
            Btn.GetComponentInChildren<Text>().text = CurInp[i] + "";           //converted CurInp array to string via + "" & added to text part of Btns  
            Btn.tag = "InputBtns";      //add tag as clones are for ip btns
            string txt = CurInp[i] + "";        //string taken to pass it 
            int temp = i;                       //temp taken to pass the BtnNo
            Btn.GetComponent<Button>().onClick.AddListener(() => IpBtnClick(txt, temp));     //On click of IpBtn, string & int are passed to IpBtnClick func
        }

        for (int i = 0; i < CurAns.Length; i++)
        {
            GameObject Btn = Instantiate(DynamicBtn, OutputBar.transform);      //clones generation as output buttons 
            Btn.tag = "OutputBtns";             //add tag as clones are for op btns 
            GameObject[] OpBtns = GameObject.FindGameObjectsWithTag("OutputBtns");
            int temp = i;                       //temp taken to pass the BtnNo
            Btn.GetComponent<Button>().onClick.AddListener(() => OpBtnClick(temp));     //On click of IpBtn, int is passed to OpBtnClick func
            OpBtns[i].GetComponentInChildren<Button>().interactable = false;
        }

        WWW image_data = new WWW("http://f28-preview.awardspace.net/pictoword.atwebpages.com/PictoWord_images/" + picture);
        yield return image_data;

      
        puzzle.GetComponent<RawImage>().texture = image_data.texture;
    }

    public void IpBtnClick(string txt,int BtnNo)
    {
        print(txt);
        GameObject[] OpBtns = GameObject.FindGameObjectsWithTag("OutputBtns");      //assign any object with those tags to OpBtns 
        GameObject[] IpBtns = GameObject.FindGameObjectsWithTag("InputBtns");       //assign any object with those tags to OpBtns

        for (int i =0; i<OpBtns.Length; i++)
        {
            string OpBtnText = OpBtns[i].GetComponentInChildren<Text>().text;   //string made by getting characters from Opbtns array one by one

            if (OpBtnText == "")    //if string is empty 
            {
                BtnPos[i] = BtnNo;      //position of input button is stored into BtnPos array 
                OpBtns[i].GetComponentInChildren<Text>().text = txt;    //get text on OpBtns on click from IpBtns 
                IpBtns[BtnNo].GetComponentInChildren<Text>().text = ""; //the text from IpBtns passed so made it blank
                IpBtns[BtnNo].GetComponentInChildren<Button>().interactable = false;
                OpBtns[i].GetComponentInChildren<Button>().interactable = true;
                break;
            }
        }

        UserAns = "";

        for(int i=0; i < OpBtns.Length; i++)
        {
            UserAns += OpBtns[i].GetComponentInChildren<Text>().text;   //as the user enters, made a user ans string consists of text of OpBtn char array
        }

        print(OpBtns.Length);
    }

    public void OpBtnClick(int BtnNo)
    {
        int IpBtnNo = BtnPos[BtnNo];        //store the letter on BtnNo pos into
        GameObject[] OpBtns = GameObject.FindGameObjectsWithTag("OutputBtns");      //assign any object with those tags to OpBtns 
        GameObject[] IpBtns = GameObject.FindGameObjectsWithTag("InputBtns");       //assign any object with those tags to OpBtns
        IpBtns[IpBtnNo].GetComponentInChildren<Text>().text = OpBtns[BtnNo].GetComponentInChildren<Text>().text;  //text of OpBtns passed to IpBtns back
        OpBtns[BtnNo].GetComponentInChildren<Text>().text = "";     //text of OpBtns removed  
        OpBtns[BtnNo].GetComponentInChildren<Button>().interactable = false;
        print("OpBtnPos" + BtnNo + "= IpBtnPos" + IpBtnNo);
        IpBtns[IpBtnNo].GetComponentInChildren<Button>().interactable = true;
         
    }

    public void SubmitBtn()
    {
        if (CurAns.Length == UserAns.Length)
        {
            if (CurAns == UserAns)
            {
                PlayObj.SetActive(false);
                SuccessObj.SetActive(true);
                print("win");

                if (PlayerPrefs.HasKey("skip_" + LevelNo))
                {
                    PlayerPrefs.DeleteKey("skip_" + LevelNo);
                }

                if (MaxLevel < LevelNo)
                {
                    MaxLevel = LevelNo;
                    PlayerPrefs.SetInt("MaxLevel", MaxLevel);
                }

                LevelNo++;
                PlayerPrefs.SetInt("LevelNo", LevelNo);
            }

            else
            {
                PlayObj.SetActive(false);
                FailObj.SetActive(true);
                print("loss");
            }
        }

    }

    public void Hint()
    {
        can.GetComponent<CanvasGroup>().interactable = false;
        score = PlayerPrefs.GetInt("score", score);

        HintObj.SetActive(true);

        HintBoard.text = hint; 

        if (score >= 20)
        {

            score -= 20;
            PlayerPrefs.SetInt("score", score);
        }

        else
        {
            score = 0;
        }

        PlayerPrefs.SetInt("score", score);
    }

    public void OkBtn()
    {
        can.GetComponent<CanvasGroup>().interactable = true;
        ScoreBoard.text = "Score = " + score;
        HintObj.SetActive(false);
        PlayObj.SetActive(true);
    }

    public void BackBtn()
    {
        SceneManager.LoadScene("Home");
    }

    public void RetryBtn()
    {
        SuccessObj.SetActive(false);
        PlayObj.SetActive(true);
    }

    public void ContinueBtn()
    {
        FailObj.SetActive(false);
        PlayObj.SetActive(true);


        score += 10;
        PlayerPrefs.SetInt("score", score);

        SceneManager.LoadScene("Play");
    }

    public void HomeBtn()
    {
        SuccessObj.SetActive(false);
        FailObj.SetActive(false);
        SceneManager.LoadScene("Home");
    }

    public void SkipLevel()
    {
        if (LevelNo > MaxLevel)
        {
            PlayerPrefs.SetInt("skip_" + LevelNo, LevelNo);
        }

        LevelNo++;
        PlayerPrefs.SetInt("LevelNo", LevelNo);
        SceneManager.LoadScene("Play");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
