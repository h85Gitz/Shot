using Manager;
using System.Collections;
using Models;
using UnityEngine;
using Utilities;

public class LaunchBullet : MonoSingleton<LaunchBullet>
{
    public delegate int VanishNum(int num);
    public GameObject hitEffect;

    CapsuleCollider capsuleCollider;

    int nums = 20;
    bool isDestory;


    Rigidbody rb;
    GameObject go;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
    }

    private void OnEnable()
    {
        if(rb == null)
        {
            go = this.gameObject;
            rb = GetComponent<Rigidbody>();
        }
        SetBulletDestory(3f);
    }


    public int GetEnemiesDeadNum(int i)
    {
        return (i += nums);
    }

    public int GetVanishNum(int num, VanishNum vanishNum)
    {
        return vanishNum(num);
    }

    public void SetBulletDestory(float time)
    {
        if(!isDestory) isDestory = true;
        StartCoroutine(DestoryButtle(time));
    }

    void OnTriggerEnter(Collider coll)
    {
        if ( coll.CompareTag("Enemy"))
        {
            GameObject hit = ObjectPool.Instance.GetPool(hitEffect);
            hit.transform.position = coll.bounds.ClosestPoint(transform.position);
        }
    }

    IEnumerator DestoryButtle (float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.Instance.PushPool(this.gameObject);
        isDestory = false;
    }

}
