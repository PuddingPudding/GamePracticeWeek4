﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float FlyingSpeed;
    public float LifeTime;
    public void InitAndShoot(Vector3 Direction)
    {
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.velocity = Direction * FlyingSpeed;
        Invoke("KillYourself", LifeTime);
    }
    public void KillYourself()
    {
        GameObject.Destroy(this.gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        KillYourself();
    }
}