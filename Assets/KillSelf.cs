using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : MonoBehaviour
{

    public float aliveTime = 1f;

    // Use this for initialization
    void Start()
    {
        Invoke("DestroySelf", aliveTime);
    }

    public void DestroySelf()
    {
        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
