using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text pointsP1Text;
    [SerializeField] private Text pointsP2Text;
    [SerializeField] private int level1Score;
    [SerializeField] private float level2Timer;
    [SerializeField] private int levelIndex;
    public static GameManager instance;
    private int currentPointsP1;
    private int currentPointsP2;
    private bool startLevel2;
    private bool bossKill;

    private void Awake()
    {
        //levelIndex = 2;
        //DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        lifeP1 = true;
        lifeP2 = true;
    }

    private bool lifeP1;
    private bool lifeP2;
    //Condicion de derrota y transicion a la pantalla de derrota
    public void OnPlayerTrulyDie(bool P1)
    {
        if (P1 == true)
        {
            lifeP1 = false;
        }
        else
        {
            lifeP2 = false;
        }
        if (lifeP1 == false && lifeP2 == false)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    


    private void Update()
    {
        //Condicion para activar el Nivel 1
        if (levelIndex == 2)
        {
            //Condicion para superar el Nivel 1
            if (currentPointsP1 + currentPointsP2 >= level1Score)
            {
                levelIndex++;
                lifeP1 = true;
                lifeP2 = true;
                SceneManager.LoadScene(levelIndex);
            }
        }
        //Condicion para pasar al Nivel 2
        else if (levelIndex == 3)
        {
            if (startLevel2 == false)
            {
                lifeP1 = true;
                lifeP2 = true;
                startLevel2 = true;
                StartCoroutine(level2()); //Rutina del Nivel 2
            }
        }
        //Condicion para pasar al Nivel 3
        else if (levelIndex == 4)
        {
            //Condicion para superar el nivel
            if (bossKill)
            {
                SceneManager.LoadScene("Victory");
                Destroy(gameObject);
            }
        }
    }

    //Funcion que devuelve la condicion necesaria del Nivel 3
    public void BossKill()
    {
        bossKill = true;
    }
    //Puntaje del jugador 1
    public int GetPointsP1()
    {
        return currentPointsP1;
    }
    //Puntaje del jugador 2
    public int GetPointsP2()
    {
        return currentPointsP2;
    }
    //Funcion que le añade el puntaje por destruir enemigos al jugador indicado
    public void AddPoints(int pointsToAdd,bool P1)
    {
        if (P1)
        {
            currentPointsP1 += pointsToAdd;
            Debug.Log($"PointsP1:{pointsToAdd}/{currentPointsP1}");
            pointsP1Text.text = currentPointsP1.ToString();
        }
        else
        {
            currentPointsP2 += pointsToAdd;
            Debug.Log($"PointsP2:{pointsToAdd}/{currentPointsP2}");
            pointsP2Text.text = currentPointsP2.ToString();
        }
    }
    IEnumerator level2()
    {
        yield return new WaitForSeconds(level2Timer);
        levelIndex++;
        SceneManager.LoadScene(levelIndex);
    }

}
