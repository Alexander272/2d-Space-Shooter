using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Guns
{
    public GameObject obj_Central_Gun, obj_Right_Gun, obj_Left_Gun;
    public ParticleSystem ps_Central_Gun, ps_Right_Gun, ps_Left_Gun;
}
public class PlayerShooting : MonoBehaviour
{
    public static PlayerShooting instance;
    public Guns guns;

    [HideInInspector]
    public int max_Power_Level_Guns = 6;
    public GameObject obj_Bullet;
    public float time_Bullet_Spawn = 0.3f;

    [HideInInspector]
    public float timer_Shot;

    [Range(1, 6)]
    public int cur_Power_Level_Guns;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        guns.ps_Central_Gun = guns.obj_Central_Gun.GetComponent<ParticleSystem>();
        guns.ps_Right_Gun = guns.obj_Right_Gun.GetComponent<ParticleSystem>();
        guns.ps_Left_Gun = guns.obj_Left_Gun.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (Time.time > timer_Shot)
        {
            timer_Shot = Time.time + time_Bullet_Spawn;
            MakeAShot();
        }
    }

    private void CreateBullet(GameObject bullet, Vector3 position_Bullet, Vector3 rotation_Bullet)
    {
        Instantiate(bullet, position_Bullet, Quaternion.Euler(rotation_Bullet));
    }

    private void MakeAShot()
    {
        switch (cur_Power_Level_Guns)
        {
            case 1:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                guns.ps_Central_Gun.Play();
                break;
            case 2:
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, Vector3.zero);
                guns.ps_Left_Gun.Play();
                guns.ps_Right_Gun.Play();
                break;
            case 3:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 5));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -5));
                guns.ps_Central_Gun.Play();
                guns.ps_Left_Gun.Play();
                guns.ps_Right_Gun.Play();
                break;
            case 4:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, Vector3.zero);
                guns.ps_Central_Gun.Play();
                guns.ps_Left_Gun.Play();
                guns.ps_Right_Gun.Play();
                break;
            case 5:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 5));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 15));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -15));
                guns.ps_Central_Gun.Play();
                guns.ps_Left_Gun.Play();
                guns.ps_Right_Gun.Play();
                break;
            case 6:
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 5));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 0));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, 0));
                guns.ps_Central_Gun.Play();
                guns.ps_Left_Gun.Play();
                guns.ps_Right_Gun.Play();
                break;
        }
    }
}
