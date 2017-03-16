using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float FlyingSpeed;
    public float LifeTime;
    public float damageValue = 15;
    public GameObject explosion;
    public AudioSource bulletAudio;

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
        other.gameObject.SendMessage("Hit", damageValue);
        //以下這一行，之所以不使用SetActive(false)的關係，是因為如果父物件Active為假，其子物件也會跟著消失
        explosion.gameObject.transform.parent = null;
        explosion.gameObject.SetActive(true);
        bulletAudio.pitch = Random.Range(0.8f, 1);
        KillYourself();
    }
}
