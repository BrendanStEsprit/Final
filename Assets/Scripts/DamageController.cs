using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public AudioClip takeDamage;
    private void OnTriggerStay2D(Collider2D other)
    {
        RubyControler controler = other.GetComponent<RubyControler>();
        if(controler != null)
        {
            controler.ChangeHealth(-1);
            

        }
    }
}
