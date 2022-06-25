using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools.Dynamic;

public class PlatformObject : MonoBehaviour
{
    private void Update() {
        if(Vector2.Distance(this.transform.position, Hero.Instance.transform.position) >= 9){
            DynamicPool.Instance.GetFromPool("PlatformObject", new Vector3(this.transform.position.x, this.transform.position.y - 15, 0));
            this.transform.position = new Vector2(0,0);
            DynamicPool.Instance.ReturnToPool("PlatformObject", this.gameObject);
        }
    }
}
