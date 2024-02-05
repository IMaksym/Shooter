using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player"); 

        if (player != null && player.GetComponent<PlayerHP>().health <= 0)
        {
            Destroy(player);
        }
    }
}