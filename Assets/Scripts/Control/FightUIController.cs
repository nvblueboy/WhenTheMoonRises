using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightUIController : MonoBehaviour {

    public Fighter player;
    public Fighter enemy;

    public Text playerHP;
    public Text playerStamina;

    private int oldPlayerHP;
    private int oldPlayerStamina;

    public Text enemyHP;
    public Text enemyStamina;

    private int oldEnemyHP;
    private int oldEnemyStamina;

	// Use this for initialization
	void Start () {
        playerHP.text = player.currHP.ToString() + "/" + player.hp.ToString();
        playerStamina.text = player.currStamina.ToString() + "/" + player.stamina.ToString();
        enemyHP.text = enemy.currHP.ToString() + "/" + enemy.hp.ToString();
        enemyStamina.text = enemy.currStamina.ToString() + "/" + enemy.stamina.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (player.currHP != oldPlayerHP)
        {
            playerHP.text = player.currHP.ToString() + "/" + player.hp.ToString();
        }

        if (player.currStamina != oldPlayerStamina)
        {
            playerStamina.text = player.currStamina.ToString() + "/" + player.stamina.ToString();
        }

        if (enemy.currHP != oldEnemyHP)
        {
            enemyHP.text = enemy.currHP.ToString() + "/" + enemy.hp.ToString();
        }

        if (enemy.currStamina != oldEnemyStamina)
        {
            enemyStamina.text = enemy.currStamina.ToString() + "/" + enemy.stamina.ToString();
        }


        oldPlayerHP = player.currHP;
        oldPlayerStamina = player.currStamina;
        oldEnemyHP = enemy.currHP;
        oldEnemyStamina = enemy.currStamina;
	}
}
