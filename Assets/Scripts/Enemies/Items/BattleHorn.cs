using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHorn : Item
{
    [Tooltip("The Opponent of the Handler")]
    public GameObject opponent;

    public override void PickUp(Handler handler)
    {
        base.PickUp(handler);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if(player == Handler.gameObject)
                continue;

            opponent = player;
        }
    }

    public override void UseItem()
    {
        base.UseItem();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Target = opponent;
            enemy.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
