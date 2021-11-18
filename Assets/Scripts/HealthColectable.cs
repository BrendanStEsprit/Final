using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthColectable : MonoBehaviour
{
    public AudioClip collectedClip;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)

    {
        RubyControler  controller = other.GetComponent<RubyControler>();
        if (controller != null)
        {
            if (controller.Health < controller.maxHealth)
            {

                controller.ChangeHealth(1);
                
                Destroy(gameObject);

                controller.PlaySound(collectedClip);
            }
        }
    }




    // Update is called once per frame

}
