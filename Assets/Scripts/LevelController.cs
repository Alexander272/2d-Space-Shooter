using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class EnemyWaves
{
    public float timeToStart;
    public GameObject wave;
    public bool is_Last_Wave;
}
public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public GameObject[] playerShip;
    public EnemyWaves[] enemyWaves;
    private bool is_Final = false;

    public GameObject panel;
    private bool isPause;
    public GameObject[] btnPause;
    public Text text_Score;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        Time.timeScale = 1;
        for (int i = 0; i< DataBase.instance.playerShipsInfo.Length; i++)
        {
            if (DataBase.instance.playerShipsInfo[i][0] == 1) LoadPlayer(i);
        }
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave, enemyWaves[i].is_Last_Wave));
        }
    }

    private void Update()
    {
        if (is_Final && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !isPause)
        {
            GamePause();
            btnPause[0].SetActive(false);
        }
        if (Player.instance == null && !isPause)
        {
            GamePause();
            btnPause[0].SetActive(false);
        }
    }

    public void ScoreInGame(int score)
    {
        DataBase.instance.Score_Game += score;
        text_Score.text = "Score: " + DataBase.instance.Score_Game.ToString();
    }

    public void LoadPlayer(int ship)
    {
        Instantiate(playerShip[ship]);
        Player.instance.player_Health = DataBase.instance.playerShipsInfo[ship][2];
        PlayerMoving.instance.speed_Player = DataBase.instance.playerShipsInfo[ship][3];
        Player.instance.shield_Health = DataBase.instance.playerShipsInfo[ship][4];
    }

    public void GamePause()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0;
            panel.SetActive(true);
            if (Player.instance != null)
            {
                btnPause[0].SetActive(true);
            }
            else
            {
                btnPause[0].SetActive(false);
            }
        }
        else
        {
            isPause = false;
            Time.timeScale = 1;
            panel.SetActive(false);
        }
    }

    public void btnRestartGame()
    {
        DataBase.instance.Score_Game = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void btnExitGame()
    {
        DataBase.instance.SaveGame();
        DataBase.instance.GameLoadScene("Menu");
    }

    IEnumerator CreateEnemyWave(float delay, GameObject Wave, bool is_Last_Wave)
    {
        if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }
        if (Player.instance != null)
        {
            Instantiate(Wave);
        }
        if (is_Last_Wave)
        {
            is_Final = true;
        }
    }
}
