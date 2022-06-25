using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamehud : MonoBehaviour
{
    private static Gamehud instance;
    public static Gamehud Instance{
        get{
            if(instance == null)instance = FindObjectOfType<Gamehud>();
            return instance;
        }
    }

    [SerializeField] Text boostHud;

    public void SetBoostText(int amount){
        boostHud.text = amount.ToString();
    }
}
