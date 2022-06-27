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

    [SerializeField] Text money, boostHud, score, scoreText, endgameText, highscoreText;
    [SerializeField] GameObject endGameObj;
    [SerializeField] RectTransform boxObj;
    [SerializeField] float movingSpeed;

    private void Update() {
        if(endGameObj.activeSelf){
            if(GameInputManager.GetKeyPress("Interaction")){
                SceneManager.LoadScene("Main");
            }
        }
        money.text = "Money: " + Hero.Instance.GetMoney().ToString();
    }

    public void SetBoostText(int amount){
        boostHud.text = amount.ToString();
    }
    public void SetScoreText(int amount){
        score.text = $"SCORE: {amount.ToString()}";
    }
    public void EndGame(int score, float percentage){
        endGameObj.SetActive(true);
        var rnd = endGameTexts[Random.Range(0, endGameTexts.Count)];
        endgameText.text = rnd;

        scoreText.text = score.ToString();
        highscoreText.text = $"{PlayerPrefs.GetInt("Highscore").ToString()}";
        Debug.Log(percentage);
        Debug.Log(330*percentage);
        StartCoroutine(MoveBox(330*percentage));
    }
    IEnumerator MoveBox(float amt){
        amt = boxObj.anchoredPosition.x + amt;
        while(boxObj.anchoredPosition.x <= amt){
            boxObj.anchoredPosition = Vector2.MoveTowards(boxObj.anchoredPosition, new Vector2(amt, boxObj.anchoredPosition.y), movingSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
