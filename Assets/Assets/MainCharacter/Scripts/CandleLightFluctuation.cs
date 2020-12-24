using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLightFluctuation : MonoBehaviour
{
    float alpha;
    bool plus = true;
    SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        alpha = 29;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //InvokeRepeating("UpdateCandleScale", 0f, .1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha >= 40 || alpha <= 20) {
            plus = !plus;
        }
    }

    void UpdateCandleScale() {
        if (plus)
        {
            Color tmp = mySpriteRenderer.color;
            tmp.a++;
            Debug.Log(mySpriteRenderer.color);
            mySpriteRenderer.color = tmp;
            alpha++;
        }
        else {
            Color tmp = mySpriteRenderer.color;
            tmp.a--;
            Debug.Log(mySpriteRenderer.color);
            mySpriteRenderer.color = tmp;
            alpha--;
        }

    }
}
