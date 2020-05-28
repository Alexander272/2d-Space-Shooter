using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region Database classes
public class DataBase
{
    public static DataBase instance = new DataBase();
    public int[][] playerShipsInfo = 
    {
        new int[] { 1, 0, 1, 10, 0, 1, 10, 0 }, // выбран, стоимость, ХП, скорость, ХП щита, начальные значения в том же проядке
        new int[] { 0, 2500, 5, 8, 0, 5, 8, 0 },
        new int[] { 0, 8000, 10, 6, 0, 10, 6, 0 }
    };
    public int constHP = 250;
    public int constSpeed = 500;
    public int constShield = 2500;
    public int Score = 100000;
    public int Score_Game = 0;

    public void GameLoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void SaveGame()
    {
        Score += Score_Game;
        for (int i = 0; i < playerShipsInfo.Length; i++)
        {
            for (int j = 0; j < playerShipsInfo[i].Length; j++)
            {
                PlayerPrefs.SetInt("InfoSave" + i + j, playerShipsInfo[i][j]);
            }
        }
        PlayerPrefs.SetInt("InfoSaveScore", Score);
    }

    public void LoadSavingGame()
    {
        for (int i = 0; i < playerShipsInfo.Length; i++)
        {
            for (int j = 0; j < playerShipsInfo[i].Length; j++)
            {
                playerShipsInfo[i][j] = PlayerPrefs.GetInt("InfoSave" + i + j);
            }
        }
        Score = PlayerPrefs.GetInt("InfoSaveScore");
    }
}
#endregion

public class MainMenu : MonoBehaviour
{
    public GameObject[] game_Panels;
    public Text Score;

    [Header("Shop panel")]
    public GameObject[] shopShips;
    public Text[] shopShipsText;
    public GameObject btnShopBuy;
    public Text shopBtnBuyCost;

    [Header("Upgrade panel")]
    public Sprite[] upgradeSpriteShips;
    public GameObject upgradeSpriteShip;
    public Slider[] upgradeSliders;
    public Text[] upgradeShowCost;
    public Text[] upgradeShowCostBtn;

    private int index;
    private int indexBuy;

    void Start()
    {
        if (PlayerPrefs.HasKey("InfoSaveScore"))
        {
            BtnLoadSavingGame();
        }
        UpdateScore();
        ShopShipHighlighting();
    }
    
    void Update()
    {
        
    }

    #region Buttons

    public void BtnSave()
    {
        DataBase.instance.SaveGame();
    }

    public void BtnLoadSavingGame()
    {
        DataBase.instance.LoadSavingGame();
    }

    public void BtnDeleteSaveGame_Debug()
    {
        PlayerPrefs.DeleteAll();
    }

    public void BtnLoadLevelGame(string name)
    {
        DataBase.instance.GameLoadScene(name);
    }

    public void BtnExitGame()
    {
        BtnSave();
        Application.Quit();
    }

    #endregion

    #region Shop

    public void ShopShipHighlighting()
    {
        for (int i = 0; i < DataBase.instance.playerShipsInfo.Length; i++)
        {
            if (DataBase.instance.playerShipsInfo[i][0] == 1)
            {
                shopShips[i].GetComponent<Image>().color = Color.white;
                shopShipsText[i].color = Color.green;
                index = i;
            }
            else
            {
                shopShips[i].GetComponent<Image>().color = Color.gray;
                shopShipsText[i].color = Color.red;
            }

            if (DataBase.instance.playerShipsInfo[i][1] == 0) shopShipsText[i].text = "Open";
            else shopShipsText[i].text = DataBase.instance.playerShipsInfo[i][1].ToString();


        }
    }

    public void ShopCheckPlayerShip(int num)
    {
        if (DataBase.instance.playerShipsInfo[num][1] == 0)
        {
            for (int i = 0; i < DataBase.instance.playerShipsInfo.Length; i++)
            {
                DataBase.instance.playerShipsInfo[i][0] = 0;
            }
            DataBase.instance.playerShipsInfo[num][0] = 1;
            index = num;
            btnShopBuy.SetActive(false);
        }
        if (DataBase.instance.playerShipsInfo[num][1] != 0 && DataBase.instance.playerShipsInfo[num][1] <= DataBase.instance.Score)
        {
            btnShopBuy.SetActive(true);
            shopBtnBuyCost.text = "Buy " + DataBase.instance.playerShipsInfo[num][1].ToString();
            indexBuy = num;
        }
        if (DataBase.instance.playerShipsInfo[num][1] != 0 && DataBase.instance.playerShipsInfo[num][1] > DataBase.instance.Score)
            btnShopBuy.SetActive(false);
        
        ShopShipHighlighting();
    }

    public void BtnShopBuyShip()
    {
        index = indexBuy;
        DataBase.instance.Score -= DataBase.instance.playerShipsInfo[index][1];
        DataBase.instance.playerShipsInfo[index][1] = 0;
        UpdateScore();
        ShopCheckPlayerShip(index);
        // for (int i = 0; i < DataBase.instance.playerShipsInfo.Length; i++)
        // {
        //     DataBase.instance.playerShipsInfo[i][0] = 0;
        // }
        // DataBase.instance.playerShipsInfo[index][1] = 1;
        // ShopShipHighlighting();
    }

    #endregion

    #region Upgrade

    public void UpgradesGetInformation()
    {
        upgradeSpriteShip.GetComponent<Image>().sprite = upgradeSpriteShips[index];
        upgradeShowCost[0].text = "Cost: " + DataBase.instance.constHP.ToString();
        upgradeShowCost[1].text = "Cost: " + DataBase.instance.constSpeed.ToString();
        upgradeShowCost[2].text = "Cost: " + DataBase.instance.constShield.ToString();
        upgradeShowCostBtn[0].text = "Cost: " + DataBase.instance.constHP.ToString();
        upgradeShowCostBtn[1].text = "Cost: " + DataBase.instance.constSpeed.ToString();
        upgradeShowCostBtn[2].text = "Cost: " + DataBase.instance.constShield.ToString();
        
        upgradeSliders[0].value = (float)(DataBase.instance.playerShipsInfo[index][2] - DataBase.instance.playerShipsInfo[index][5]) / 7;
        upgradeSliders[1].value = (float)(DataBase.instance.playerShipsInfo[index][3] - DataBase.instance.playerShipsInfo[index][6]) / 9;
        upgradeSliders[2].value = (float)(DataBase.instance.playerShipsInfo[index][4] - DataBase.instance.playerShipsInfo[index][7]) / 5;
    }

    public void BtnUpgrade(int idx)
    {
        if (idx == 0 && DataBase.instance.Score > DataBase.instance.constHP && upgradeSliders[0].value < 1)
        {
            DataBase.instance.Score -= DataBase.instance.constHP;
            DataBase.instance.constHP *= 2;
            DataBase.instance.playerShipsInfo[index][2] += 1;
            upgradeShowCost[0].text = "Cost: " + DataBase.instance.constHP;
            upgradeShowCostBtn[0].text = "Cost: " + DataBase.instance.constHP;
            upgradeSliders[0].value += (float)1 / 7;
        }
        if (idx == 1 && DataBase.instance.Score > DataBase.instance.constSpeed && upgradeSliders[1].value < 1)
        {
            DataBase.instance.Score -= DataBase.instance.constSpeed;
            DataBase.instance.constSpeed *= 2;
            DataBase.instance.playerShipsInfo[index][3] += 1;
            upgradeShowCost[1].text = "Cost: " + DataBase.instance.constSpeed;
            upgradeShowCostBtn[1].text = "Cost: " + DataBase.instance.constSpeed;
            upgradeSliders[1].value += (float)1 / 9;
        }
        if (idx == 2 && DataBase.instance.Score > DataBase.instance.constShield && upgradeSliders[2].value < 1)
        {
            DataBase.instance.Score -= DataBase.instance.constShield;
            DataBase.instance.constShield *= 2;
            DataBase.instance.playerShipsInfo[index][4] += 1;
            upgradeShowCost[2].text = "Cost: " + DataBase.instance.constShield;
            upgradeShowCostBtn[2].text = "Cost: " + DataBase.instance.constShield;
            upgradeSliders[2].value += (float)1 / 5;
        }
        UpdateScore();
    }

    #endregion

    public void UpdateScore()
    {
        Score.text = DataBase.instance.Score.ToString();
    }

    public void Show_Panel(int index)
    {
        ShopShipHighlighting();
        game_Panels[index].SetActive(true);
    }

    public void Hide_Panel(int index)
    {
        BtnSave();
        game_Panels[index].SetActive(false);
    }
}
