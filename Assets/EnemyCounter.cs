using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI remainingEnemies;
    [SerializeField] private TextMeshProUGUI currentHealth;
    [SerializeField] GameManager gm;

    public static EnemyCounter singleton;

    int enemyCount = 0;
    int playerHealth = 0;

    void Awake(){
        if(singleton != null){
            Destroy(this.gameObject);
        }
        singleton=this;
    }

    public void CountEnemy(){
        enemyCount += 1;
        DisplayRemainingEnemies();
    }

    public void enemyKill(){
        enemyCount -= 1;
        DisplayRemainingEnemies();
        if(enemyCount <= 0){
            gm.Victory();
        }
    }

    public void DisplayRemainingEnemies(){
        remainingEnemies.text = enemyCount.ToString();
    }

    public void SetHealth(int amount){
        playerHealth += amount;
        ShowHealth();
    }

    public void SubtractHealth(int amount){
        playerHealth -= amount;
        ShowHealth();
    }

    public void ShowHealth(){
        currentHealth.text = playerHealth.ToString();
    }
}
