using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BigasTools;

public class Engine : MonoBehaviour
{
    private static Engine instance;
    public static Engine Instance{
        get{
            if(instance == null)instance = FindObjectOfType<Engine>();
            return instance;
        }
    }
    private void Awake() {
        GameInputKeys.CreateDictionarys();
        GameInputKeys.CreateGamepad();
        ResourceController.StartSets();
    }
    void Start(){
        
    }
    public void SaveScore(int score){
        var s = PlayerPrefs.GetInt("Highscore");
        if(s <= score){
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

}
