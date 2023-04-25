using System.Collections;

using Models;
using Test.SubscriberModel;
using UnityEngine;

public class Muzzle : MonoBehaviour
{

    public GameObject bulletPrefab;

    public Transform[] muzzles;
    [Header("������Ч")]
    public AudioClip effectAudioclip;
    public GameObject bulletEffect;

    public Transform effectPos;
    public int bulletSpeed;

    [Header("��������ʱ��")]
    public float FireTime;//���ǵ�ҩ��������
    public float FireRate ;

    private float FireTimeLeft;
    private float LastFire = -10f;


    bool isLauching;
    int i;

    void Start()
    {
        int nums = transform.childCount;
        muzzles = new Transform[nums];
        for ( i = 0; i < nums; i++)
        {
            muzzles[i] = transform.GetChild(i);
        }

    }


    void Update()
    {
        PlayerMove.Instance.LookAtMouse();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Stackle<string>.Instance.stackleEvent += PlayerMove.Instance.Subscribe;
            Stackle<string>.Instance.add("test");

            BoomEffect();
            if (Time.time > LastFire + FireRate)
            {
               ReadyToLaunch();
            }
        }
        Launch();
    }


    void ReadyToLaunch()
    {
        isLauching = true;
        FireTimeLeft = FireTime;
        LastFire = Time.time;
    }

    public void Launch( )
    {
        if (isLauching)
        {
            if (FireTimeLeft > 0)
            {
                foreach (var item in muzzles)
                {
                    GameObject go = ObjectPool.Instance.GetPool(bulletPrefab);

                    go.GetComponent<Rigidbody>().AddForce(item.transform.forward * bulletSpeed, ForceMode.Impulse);
                    FireTimeLeft -= Time.deltaTime;
                    go.transform.position = item.transform.position;
                    //������������ӵ�����ת����ô����������ǰ���õķ����䣬������ǹ�ڵķ���
                    if (PlayerMove.Instance.isRotate)
                    {
                        Quaternion offset = Quaternion.LookRotation(PlayerMove.Instance.target - item.transform.position);
                        item.rotation = Quaternion.Slerp(item.rotation, offset, Time.deltaTime * 5.0f);
                    }

                    go.transform.rotation = item.transform.rotation;


                    Debug.Log(" rb.velocity " + go.GetComponent<Rigidbody>().velocity);

                    StartCoroutine(BulletAudio());
                }
            }
            isLauching = false;
        }
    }


    IEnumerator BulletAudio()
    {
      yield return new WaitForSeconds(0.5f);
      AudioSource.PlayClipAtPoint(effectAudioclip, transform.position);
    }

    void BoomEffect()
    {
        if (isLauching || FireTimeLeft > 0)
        {
            GameObject effect = ObjectPool.Instance.GetPool(bulletEffect);
            effect.transform.position = effectPos.transform.position;
            effect.transform.rotation = Quaternion.identity;
        }
    }
}
