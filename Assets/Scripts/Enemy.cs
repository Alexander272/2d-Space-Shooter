using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemy_Health;
    public int score_Value;

    [Space]
    public GameObject obj_Bullet;
    public float shot_Time_Min, shot_Time_Max;
    public int shot_Chance;

    [Header("Boss")]
    public bool is_Boss;
    public GameObject obj_Bullet_Boss;
    public float time_Bullet_Boss_Spawn;
    private float timer_Shot_Boss = 8;
    public int shot_Chance_Boss;

    void Start()
    {
        InvokeRepeating("OpenFire", Random.Range(shot_Time_Min, shot_Time_Max), Random.Range(shot_Time_Min, shot_Time_Max));
    }

    private void Update()
    {
        if (is_Boss)
        {
            if (Time.time > timer_Shot_Boss)
            {
                timer_Shot_Boss = Time.time + time_Bullet_Boss_Spawn;
                OpenFireBoss();
            }
        }
    }

    private void OpenFireBoss()
    {
        if (Random.value < (float)shot_Chance_Boss / 100)
        {
            for (int angleZ = -40; angleZ < 41; angleZ += 10)
            {
                Instantiate(obj_Bullet_Boss, transform.position, Quaternion.Euler(0, 0, angleZ));
            }
        }
    }

    public void OpenFire()
    {
        if (Random.value < (float)shot_Chance / 100)
        {
            Instantiate(obj_Bullet, transform.position, Quaternion.identity);
        }
    }

    public void GetDamage(int damage)
    {
        enemy_Health -= damage;
        if (enemy_Health <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        LevelController.instance.ScoreInGame(score_Value);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            GetDamage(1);
            if (Player.instance.shield_Health != 0)
            {
                Player.instance.GetDamageShield(1);
            }
            else
            {
                Player.instance.GetDamage(1);
            }
        }
    }
}
