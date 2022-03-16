using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

  [SerializeField] private GameObject _smokeSign;
  
  private void OnCollisionEnter2D(Collision2D collision)
    {
        //The bat collision is deadly!
        if(collision.collider.GetComponent<Bat>() != null)
        {
            Instantiate(_smokeSign, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        //Do nothing if you collide to an enemy
        Enemy enemy = collision.collider.GetComponent<Enemy>();

        if(enemy != null)
        {
            return;
        }

        //Smashed by the obstacles
        if(collision.contacts[0].normal.y < -0.5)
        {
            Instantiate(_smokeSign, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
