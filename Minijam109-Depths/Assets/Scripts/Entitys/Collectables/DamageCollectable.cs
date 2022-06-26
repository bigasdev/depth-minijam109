using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollectable : Collectable
{
    [SerializeField] float velocityPercentage;
    [SerializeField] int damageAmt;
    public override void OnCollect()
    {
        Hero.Instance.ChangeVelocity(velocityPercentage);
        Hero.Instance.ChangeBoost(-damageAmt);
        base.OnCollect();
    }
}
