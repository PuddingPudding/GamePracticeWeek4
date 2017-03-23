using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{

    public UnityEngine.UI.Image BlackCover;
    public Image BloodBlur;
    public Text HPText;
    // Use this for initialization
    void Start()
    {
        BlackCover.color = Color.black;
        DOTween.To(() => BlackCover.color, (x) => BlackCover.color = x, new Color(0, 0, 0, 0), 1f);
    }
    Tweener tweenAnimation;
    public void PlayHitAnimation()
    {
        if (tweenAnimation != null)
            tweenAnimation.Kill();

        BloodBlur.color = Color.white;
        tweenAnimation = DOTween.To(() => BloodBlur.color, (x) => BloodBlur.color = x, new Color(1, 1, 1, 0), 0.5f);
    }
    public void PlayerDiedAnimation()
    {
        BloodBlur.color = Color.white;
        DOTween.To(() => BlackCover.color, (x) => BlackCover.color = x, new Color(0, 0, 0, 1), 1f).SetDelay(3);
    }
    public void SetHP(int hp)
    {
        HPText.text = "HP:" + hp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
