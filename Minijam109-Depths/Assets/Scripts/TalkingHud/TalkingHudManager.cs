using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingHudManager : MonoBehaviour
{
    private static TalkingHudManager instance;
    public static TalkingHudManager Instance{
        get{
            if(instance== null)instance = FindObjectOfType<TalkingHudManager>();
            return instance;
        }
    }
    public TalkingHud hud;
    public void InitializeHud(Transform obj, string txt){
        if(hud!=null)return;
        var hudObj = Resources.Load<TalkingHud>("Prefabs/TalkingHud");
        hud = Instantiate(hudObj);
        hud.onEnd += () =>{
            Destroy(hud.gameObject);
            this.hud = null;
        };
        hud.transform.SetParent(obj);
        hud.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + 2);
        hud.Initialize(txt);
    }
}
