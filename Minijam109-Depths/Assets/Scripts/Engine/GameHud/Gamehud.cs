using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamehud : MonoBehaviour
{
    public List<string> endGameTexts;
    private static Gamehud instance;
    public static Gamehud Instance{
        get{
            if(instance == null)instance = FindObjectOfType<Gamehud>();
            return instance;
        }
    }

    [SerializeField] Text boostHud, score, endgameText, highscoreText;
    [SerializeField] GameObject endGameObj;

    private void Update() {
        if(endGameObj.activeSelf){
            if(GameInputManager.GetKeyPress("Interaction")){
                SceneManager.LoadScene("Main");
            }
        }
    }

    public void SetBoostText(int amount){
        boostHud.text = amount.ToString();
    }
    public void SetScoreText(int amount){
        score.text = $"SCORE: {amount.ToString()}";
    }
    public void EndGame(){
        endGameObj.SetActive(true);
        var rnd = endGameTexts[Random.Range(0, endGameTexts.Count)];
        endgameText.text = rnd;

        highscoreText.text = $"HIGHSCORE: {PlayerPrefs.GetInt("Highscore").ToString()}";
    }
}
