using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum gameStatus
{
    next, play, gameover, win
}
public class Manager : Loader<Manager>
{
    [SerializeField]
    int totalWaves = 3;
    [SerializeField]
    TMP_Text totalMoneyLabel;
    [SerializeField]
    TMP_Text currentWave;
    [SerializeField]
    TMP_Text totalEscapedLabel;
    [SerializeField]
    TMP_Text playBtnLabel;
    [SerializeField]
    Button playBtn;
    [SerializeField]
    GameObject spawnPoint;
    [SerializeField]
    GameObject[] enemies;
    [SerializeField]
    int maxEnemiesOnScreen;
    [SerializeField]
    int totalEnemies;
    [SerializeField]
    int enemiesPerSpawn;

    int waveNumver = 0;
    int totalMoney = 10;
    int totalEscaped = 0;
    int roundEscaped = 0;
    int totalKilled;
    int whichEnemiesToSpawn = 0;
    gameStatus currentState = gameStatus.play;

    public List<Enemy> EnemyList = new List<Enemy>();

    const float spawnDelay = 0.5f;

    public int TotalMoney
    {
        get { return totalMoney; }
        set 
        { 
            totalMoney = value; 
            totalMoneyLabel.text = TotalMoney.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playBtn.gameObject.SetActive(false);
        ShowMenu();
    }
    private void Update()
    {
        HandleEscape();
    }
    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }
    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }
    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
    public void DestroyEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }

        EnemyList.Clear();
    } 
    public void addMoney(int amount)
    {
        TotalMoney += amount;
    }
    public void substractMoney(int amount)
    {
        TotalMoney -= amount;
    }
    public void ShowMenu()
    {
        switch (currentState)
        {
            case gameStatus.gameover:
                playBtnLabel.text = "Play again!";

                break;
            case gameStatus.next:
                playBtnLabel.text = "Next wave";

                break;
            case gameStatus.play:
                playBtnLabel.text = "Play game";

                break;
            case gameStatus.win:
                playBtnLabel.text = "Play game";

                break;
        }
        playBtn.gameObject.SetActive(true);
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.DisabledDrag();
            TowerManager.Instance.towerBTNPressed = null;
        }
    }

}
