using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (PlayerShooting.instance.cur_Power_Level_Guns < PlayerShooting.instance.max_Power_Level_Guns)
            {
                PlayerShooting.instance.cur_Power_Level_Guns++;
            }
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
