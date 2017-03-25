using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{

    public float MinimumShootPeriod; //最短射擊間隔
    public float muzzleShowPeriod; //閃爍間隔
    private float shootCounter = 0;
    private float muzzleCounter = 0;
    public GameObject muzzleFlash;
    public GameObject bulletCandidate;

    private AudioSource gunShootSound;

    public void Start()
    {
        gunShootSound = this.GetComponent<AudioSource>();
    }
    public void TryToTriggerGun()
    {
        if (shootCounter <= 0)
        {
            gunShootSound.Stop();
            gunShootSound.pitch = Random.Range(0.8f, 1);  //開槍前先隨機調整開槍聲的音高
            gunShootSound.Play();

            this.transform.DOShakeRotation(MinimumShootPeriod * 0.8f, 3f);
            muzzleCounter = muzzleShowPeriod;
            //讓閃爍特效在每次出現時都隨機旋轉
            muzzleFlash.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            shootCounter = MinimumShootPeriod;
            GameObject newBullet = GameObject.Instantiate(bulletCandidate);
            BulletScript bullet = newBullet.GetComponent<BulletScript>();
            bullet.transform.position = muzzleFlash.transform.position;
            bullet.transform.rotation = muzzleFlash.transform.rotation;
            bullet.InitAndShoot(muzzleFlash.transform.forward);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (shootCounter > 0)
        {
            shootCounter -= Time.deltaTime;
        }
        if (muzzleCounter > 0)
        {
            muzzleFlash.gameObject.SetActive(true);
            muzzleCounter -= Time.deltaTime;
        }
        else
        {
            muzzleFlash.gameObject.SetActive(false);
        }
    }
}
