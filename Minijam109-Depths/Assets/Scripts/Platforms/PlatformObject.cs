using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigasTools.Dynamic;
using BigasTools;

public class PlatformObject : MonoBehaviour
{
    [SerializeField] Vector2 bounds = new Vector2(5, 10);
    public void Initialize(){
        var dmgRandomValue = Random.Range(0, 2);
        var boostRandomValue = Random.Range(0, 2);
        var coinRandomValue = Random.Range(1,3);

        for (int i = 0; i < dmgRandomValue; i++)
        {
            var pos = new Vector2(this.transform.position.x + Random.Range(-bounds.x, bounds.x), this.transform.position.y + Random.Range(-bounds.y, bounds.y));
            var c = PoolsManager.Instance.GetPool("DamageCollectables").GetFromPool(pos);

            c.GetComponentInParent<DamageCollectable>().onCollect += () =>{
                PoolsManager.Instance.GetPool("DamageCollectables").AddToPool(c);
            };
        }
        for (int i = 0; i < boostRandomValue; i++)
        {
            var pos = new Vector2(this.transform.position.x + Random.Range(-bounds.x, bounds.x), this.transform.position.y + Random.Range(-bounds.y, bounds.y));
            var c = PoolsManager.Instance.GetPool("BoostCollectables").GetFromPool(pos);

            c.GetComponentInParent<BoostCollectable>().onCollect += () =>{
                PoolsManager.Instance.GetPool("BoostCollectables").AddToPool(c);
            };
        }
        for (int i = 0; i < coinRandomValue; i++)
        {
            var pos = new Vector2(this.transform.position.x + Random.Range(-bounds.x, bounds.x), this.transform.position.y + Random.Range(-bounds.y, bounds.y));
            var c = PoolsManager.Instance.GetPool("CoinCollectables").GetFromPool(pos);

            c.GetComponentInParent<CoinCollectable>().onCollect += () =>{
                PoolsManager.Instance.GetPool("CoinCollectables").AddToPool(c);
            };
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        var h = other.GetComponentInParent<Hero>();
        if(h == null)return;
        var p = DynamicPool.Instance.GetFromPool("PlatformObject", new Vector3(this.transform.position.x, this.transform.position.y - 18, 0));
        p.GetComponent<PlatformObject>()?.Initialize();
        this.transform.position = new Vector2(0,0);
        DynamicPool.Instance.ReturnToPool("PlatformObject", this.gameObject);
    }
}
