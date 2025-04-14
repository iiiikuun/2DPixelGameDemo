using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Trigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            player.skill.dash.CloneWhenCollideEnemy();
        }
    }
}
