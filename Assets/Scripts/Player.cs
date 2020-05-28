using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    public int player_Health = 1;
    public GameObject obj_Shield;
    public int shield_Health = 1;

    public int max_Player_Health = 1;
    public int max_Shield_Health = 1;

    private Slider slider_HP_Player;
    private Slider slider_Shield_Player;

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
         
        for (int i = 0; i < DataBase.instance.playerShipsInfo.Length; i++)
        {
            if (DataBase.instance.playerShipsInfo[i][0] == 1) {
                max_Player_Health = DataBase.instance.playerShipsInfo[i][2];
                max_Shield_Health = DataBase.instance.playerShipsInfo[i][4];
            }
        }

        slider_HP_Player = GameObject.FindGameObjectWithTag("Sl_HP").GetComponent<Slider>();
        slider_Shield_Player = GameObject.FindGameObjectWithTag("Sl_Shield").GetComponent<Slider>();
    }

    private void Start()
    {
        slider_HP_Player.value = 1f;
        if (shield_Health != 0)
        {
            obj_Shield.SetActive(true);
            slider_Shield_Player.value = 1f;
        }
        else
        {
            obj_Shield.SetActive(false);
            slider_Shield_Player.value = 0f;
        }
    }
    
    void Update()
    {
        
    }

    public void GetDamageShield(int damage)
    {
        shield_Health -= damage;
        slider_Shield_Player.value = (float)shield_Health / max_Shield_Health;
        if (shield_Health <= 0)
        {
            obj_Shield.SetActive(false);
        }
    }

    public void GetDamage(int damage)
    {
        player_Health -= damage;
        slider_HP_Player.value = (float)player_Health / max_Player_Health;
        if (player_Health <= 0)
        {
            Destruction();
        }
    }

    void Destruction()
    {
        Destroy(gameObject);
    }
}
