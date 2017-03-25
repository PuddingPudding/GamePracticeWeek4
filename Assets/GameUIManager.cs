using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        DOTween.To(() => BlackCover.color, (x) => BlackCover.color = x, Color.clear, 1f);
        //以上這一行，在遊戲開始時，該UI會被生成，同時會執行Start的內容，此行意思為，在1秒內
    }
    Tweener tweenAnimation;
    public void PlayHitAnimation()
    {
        if (tweenAnimation != null)
            tweenAnimation.Kill();

        BloodBlur.color = Color.white;
        tweenAnimation = DOTween.To(() => BloodBlur.color, (x) => BloodBlur.color = x, Color.clear, 0.5f);
        //DOTween.To(() => BloodBlur.color, (x) => BloodBlur.color = x, Color.clear, 0.5f);
    }
    public void PlayerDiedAnimation()
    {
        BloodBlur.color = Color.white;  //玩家死亡時，先讓血腥特效顯示出來
        //以下此行，在血腥特效常駐後，先等3秒(後面的SetDelay(3) )，接著，在進行 (1秒內將顏色轉為黑色) 這個動作
        DOTween.To(() => BlackCover.color, (x) => BlackCover.color = x, Color.black, 1f).SetDelay(3).OnComplete(() =>
        {
            DOTween.To(() => BloodBlur.color, (x) => BloodBlur.color = x, Color.clear, 0.5f).SetDelay(3).OnComplete(() =>
            {
                SceneManager.LoadScene("PrewScene");
            });
        });
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
