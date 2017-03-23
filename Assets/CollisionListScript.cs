using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionListScript : MonoBehaviour
{
    public List<Collider> CollisionObjects; //碰撞器(Collider)為固有型態，我猜它已經有自己做過序列化(Serializable)了
    public void OnTriggerEnter(Collider other)
    {
        CollisionObjects.Add(other);
    }
    public void OnTriggerExit(Collider other)
    {
        CollisionObjects.Remove(other);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
