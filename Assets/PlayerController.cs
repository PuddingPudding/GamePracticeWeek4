using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animatorController;

    // Use this for initialization
    void Start()
    {
        animatorController = this.GetComponent<Animator>();
    }

    public float MoveSpeed;
    float currentSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        float result = 0;
        if (Input.GetKey(KeyCode.W))
        {
            result += MoveSpeed;
        }
        if(Input.GetKey(KeyCode.S))
        {
            result -= MoveSpeed;
        }
        currentSpeed = result;
        this.transform.position += Time.deltaTime * currentSpeed * this.transform.forward;
        animatorController.SetFloat("Speed", currentSpeed);
    }
}
