using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    private BoxCollider2D _boundare_Collider;
    private Vector2 _viewport_Size;

    private void Awake()
    {
        _boundare_Collider = GetComponent<BoxCollider2D>();
    }
    
    void Start()
    {
        ResizeCollider();
    }

    void ResizeCollider()
    {
        _viewport_Size = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;
        _viewport_Size.x *= 1.5f;
        _viewport_Size.y *= 1.5f;
        _boundare_Collider.size = _viewport_Size;
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        switch(coll.tag)
        {
            case "Planet": 
                Destroy(coll.gameObject);
                break;
            case "Bullet": 
                Destroy(coll.gameObject);
                break;
            case "Bonus": 
                Destroy(coll.gameObject);
                break;
        }
    }
}
