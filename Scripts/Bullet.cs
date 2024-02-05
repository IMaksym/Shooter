using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private void OnCollisionEnter(Collision collision)
   {
        Transform hitTransform = collision.transform;
        if( hitTransform.CompareTag("Player"))
        {
            hitTransform.GetComponent<PlayerHP>().TakeDamage(10);
        }
        else if (hitTransform.CompareTag("Enemy"))
        {
            
        }
        Destroy(gameObject);
   }
}
