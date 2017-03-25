using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animatorController;

    public Transform rotateYTransform;
    public Transform rotateXTransform;
    public float rotateSpeed;
    public float currentRotateX = 0;
    float currentSpeed = 0;
    public float moveSpeed;

    public Rigidbody rigidBody;

    public JumpSensor jumpSensor;
    public float jumpSpeed;

    public GunManager gunManager;

    public GameUIManager uiManager;
    public int hp = 100;

    // Use this for initialization
    void Start()
    {
        animatorController = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        if (Input.GetMouseButton(0))
        {
            gunManager.TryToTriggerGun();
        }

        //決定鍵盤input的結果
        Vector3 movDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) { movDirection.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movDirection.z -= 1; }
        if (Input.GetKey(KeyCode.D)) { movDirection.x += 1; }
        if (Input.GetKey(KeyCode.A)) { movDirection.x -= 1; }
        movDirection = movDirection.normalized;

        //決定要給Animator的動畫參數
        if (movDirection.magnitude == 0 || !jumpSensor.IsCanJump() )
        {
            currentSpeed = 0;
        }
        else
        {
            if (movDirection.z < 0)
            {
                currentSpeed = -moveSpeed;
            }
            else
            {
                currentSpeed = moveSpeed;
            }
        }
        animatorController.SetFloat("Speed", currentSpeed);

        //以下將移動方向轉換成自己所面對的方向
        Vector3 worldSpaceDirection = movDirection.z * rotateYTransform.transform.forward +
                                      movDirection.x * rotateYTransform.transform.right;
        Vector3 velocity = rigidBody.velocity;
        velocity.x = worldSpaceDirection.x * moveSpeed;
        velocity.z = worldSpaceDirection.z * moveSpeed;
        //以上兩行將x和z的速度個別獨立出來，是為了避免去動到跳躍的向量
        //rigidBody.velocity = velocity;                
        //Vector3 velocity = worldSpaceDirection * moveSpeed;

        if (Input.GetKey(KeyCode.Space) && jumpSensor.IsCanJump())
        {
            velocity.y = jumpSpeed;
        }
        rigidBody.velocity = velocity;

        //計算滑鼠角度，先將水平軸與垂直軸的數值吃進來(計算滑鼠移動的量)，再去乘以旋轉速度(可當成滑鼠靈敏度)
        rotateYTransform.transform.localEulerAngles += new Vector3(0, Input.GetAxis("Horizontal"), 0) * rotateSpeed;
        currentRotateX += Input.GetAxis("Vertical") * rotateSpeed;
        if (currentRotateX > 90)
        {
            currentRotateX = 90;
        }
        else if (currentRotateX < -90)
        {
            currentRotateX = -90;
        }
        rotateXTransform.transform.localEulerAngles = new Vector3(-currentRotateX, 0, 0);
    }

    public void Hit(int value)
    {
        if (hp <= 0)
        {
            return;
        }
        hp -= value;
        uiManager.SetHP(hp);
        if (hp > 0)
        {
            uiManager.PlayHitAnimation();
        }
        else
        {
            uiManager.PlayerDiedAnimation();
            rigidBody.gameObject.GetComponent<Collider>().enabled = false;
            rigidBody.useGravity = false;
            rigidBody.velocity = Vector3.zero;
            this.enabled = false; //讓該物件不再updated
            rotateXTransform.transform.DOLocalRotate(new Vector3(-60, 0, 0), 0.5f);
            //在0.5秒內向後仰60度，製造倒下的效果
            rotateYTransform.transform.DOLocalMoveY(-1.5f, 0.5f).SetRelative(true);
            //在0.5秒內將y軸向下降低1.5，後面補SetRelative代表著，我不是0.5秒內y座標移到-1.5，而是根據原本的y座標減少1.5
        }
    }
}
