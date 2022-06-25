using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSquash : MonoBehaviour
{
    public Transform sprite;

    public float xFactor = 1f, yFactor = 1f;
    [SerializeField] float velocityFactor = .035f;
    public bool squashed = false;
    private void Update()
    {
        ScaleReference();
        ReturnToOriginalScale();
    }
    public void Squash(float x, float y)
    {
        squashed = true;
        xFactor = x;
        yFactor = y;
    }
    void ScaleReference()
    {
        sprite.localScale = new Vector2(xFactor, yFactor);
    }
    void ReturnToOriginalScale()
    {
        if(!squashed)return;
        if(xFactor >= .92f && xFactor <= 1f && yFactor >= .92f && yFactor <= 1f){
            squashed = false;
        }
        if (xFactor <= 1f)
        {
            xFactor += velocityFactor;
        }

        if (xFactor >= 1f)
        {
            xFactor -= velocityFactor;
        }

        if (yFactor <= .99f)
        {
            yFactor += velocityFactor;
        }
        if (yFactor >= .99f)
        {
            yFactor -= velocityFactor;
        }

    }
}

