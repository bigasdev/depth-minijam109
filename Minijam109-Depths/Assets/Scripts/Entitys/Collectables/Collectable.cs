using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools.Dynamic;
using BigasTools.Sprite2D;
using BigasTools;
public class Collectable : MonoBehaviour
{
    public System.Action onCollect = delegate{};
    private void OnTriggerEnter2D(Collider2D other) {
        var h = other.GetComponentInParent<Hero>();
        if(h == null)return;
        OnCollect();
    }
    public virtual void OnCollect(){
        AudioController.Instance.PlaySound("pickingStuff");
        var c = DynamicPool.Instance.GetFromPool("ExplosionParticle", Hero.Instance.transform.position);
        c.GetComponent<SpriteAnimator>().idleAnimation.onStop += () =>{
            DynamicPool.Instance.ReturnToPool("ExplosionParticle", c);
        };
        if(onCollect!=null){
            onCollect();
            onCollect = null;
        }
    }
}
