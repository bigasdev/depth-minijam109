using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public System.Action onCollect = delegate{};
    private void OnTriggerEnter2D(Collider2D other) {
        var h = other.GetComponentInParent<Hero>();
        if(h == null)return;
        OnCollect();
    }
    public virtual void OnCollect(){
        if(onCollect!=null){
            onCollect();
            onCollect = null;
        }
    }
}
