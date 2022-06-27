using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : Collectable
{
    int coinAmount = 0;
    private void Start() {
        coinAmount = Random.Range(1, 5);
    }
    public override void OnCollect()
    {
        Hero.Instance.AddMoney(coinAmount);
        base.OnCollect();
    }
}
