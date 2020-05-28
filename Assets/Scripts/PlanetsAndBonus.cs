using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsAndBonus : MonoBehaviour
{
    public GameObject[] obj_Bonus;
    public float start_Time_Bonus_Spawn;
    public float end_Time_Bonus_Spawn;
    public GameObject[] obj_Planets;
    public float start_Time_Planet_Spawn;
    public float end_Time_Planet_Spawn;
    public float speed_Planet;

    List<GameObject> planetList = new List<GameObject>();
    List<GameObject> bonusList = new List<GameObject>();
    private void Start()
    {
        StartCoroutine(BonusCreation());
        StartCoroutine(PlanetsCreation());
    }

    IEnumerator BonusCreation()
    {
        for (int i = 0; i < obj_Bonus.Length; i++)
        {
            bonusList.Add(obj_Bonus[i]);
        }

        while(true)
        {
            yield return new WaitForSeconds(Random.Range(start_Time_Bonus_Spawn, end_Time_Bonus_Spawn));
            int randomIndex = Random.Range(0, bonusList.Count);
            Instantiate(bonusList[randomIndex], new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX), PlayerMoving.instance.borders.maxY * 2f), Quaternion.identity);
        }
    }

    IEnumerator PlanetsCreation()
    {
        for (int i = 0; i < obj_Planets.Length; i++)
        {
            planetList.Add(obj_Planets[i]);
        }

        yield return new WaitForSeconds(7);

        while(true)
        {
            int randomIndex = Random.Range(0, planetList.Count);
            GameObject newPlanet = Instantiate(planetList[randomIndex],  new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX), PlayerMoving.instance.borders.maxY * 2f), Quaternion.Euler(0, 0, Random.Range(-25, 25)));

            planetList.RemoveAt(randomIndex);
            if (planetList.Count == 0) {
                for (int i = 0; i < obj_Planets.Length; i++)
                {
                    planetList.Add(obj_Planets[i]);
                }
            }

            newPlanet.GetComponent<ObjectMoving>().speed = speed_Planet;

            yield return new WaitForSeconds(Random.Range(start_Time_Planet_Spawn, end_Time_Planet_Spawn));
        }
    }

}
