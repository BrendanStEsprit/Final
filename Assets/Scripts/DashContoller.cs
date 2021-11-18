using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashContoller : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        BotController e = other.collider.GetComponent<BotController>();

        if (e != null)
        {
            e.Fix();
            //controller.PlaySound(projectileClip);
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
