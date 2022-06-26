using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField] SpriteRenderer bar;
    Hero hero;
    void Start()
    {
        hero = Hero.Instance;
    }
    private void Update() {
        if(hero == null)return;
        Debug.Log(hero.GetJumpAmt());
        bar.transform.localScale = new Vector2(1, hero.GetJumpAmt());
        if(Vector2.Distance(this.transform.position, hero.transform.position) >= 4)return;
        if(Vector2.Distance(this.transform.position, hero.transform.position) <= 1f){
            hero.spriteSquash.Squash(.75f, 1f);
            hero.SetChargeState(true);
            return;
        }
        hero.SetChargeState(false);
    }
}
