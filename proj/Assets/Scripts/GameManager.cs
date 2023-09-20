using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public int gold;
    public float HP = 100;

    // player status
    public float MoveSpeed = 1;
    public float LifeSteal = 0;
    public float AttackSpeed = 1;
    public float DamageMultiplier = 1;

    // manage different scene
    public int currentLevel = 0;
    public GameObject activeAlien1;
    public GameObject activeAlien2;
    public GameObject activeAlien3;

    // manage dropped item lists
    public GameObject dropItem1;
    public GameObject dropItem2;
    public GameObject dropItem3;
    public float DropPossibility = 0.4f;

    public bool hasDouble = false;
    public bool hasExplosion = false;

    // Score
    public Text scoreText;
    public Text goldText;
    public Text PlayerHP;

    // Scene Management
    public int currLevel;
    public int prevLevel;

    // resource management
    public GameObject Level1Alien1;
    public GameObject Level1Alien2;
    public GameObject Level1Alien3;

    public GameObject Level2Alien1;
    public GameObject Level2Alien2;
    public GameObject Level2Alien3;

    public GameObject Level3Alien1;
    public GameObject Level3Alien2;
    public GameObject Level3Alien3;


    public GameObject Level1Drop1;
    public GameObject Level1Drop2;
    public GameObject Level1Drop3;

    public GameObject Level2Drop1;
    public GameObject Level2Drop2;
    public GameObject Level2Drop3;

    public GameObject Level3Drop1;
    public GameObject Level3Drop2;
    public GameObject Level3Drop3;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        OnSceneLoaded();
        /*scoreText = GameObject.Find("ScoreObject").GetComponent<Text>();
        goldText = GameObject.Find("GoldObject").GetComponent<Text>();*/
    }
    

    public void OnSceneLoaded()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        switch(currentLevel)
        {
            case SceneId.LEVEL1:
                activeAlien1 = Level1Alien1;
                activeAlien2 = Level1Alien2;
                activeAlien3 = Level1Alien3;

                dropItem1 = Level1Drop1;
                dropItem2 = Level1Drop2;
                dropItem3 = Level1Drop3;
                break;
            case SceneId.LEVEL2:
                activeAlien1 = Level2Alien1;
                activeAlien2 = Level2Alien2;
                activeAlien3 = Level2Alien3;

                dropItem1 = Level2Drop1;
                dropItem2 = Level2Drop2;
                dropItem3 = Level2Drop3;
                break;
            case SceneId.LEVEL3:
                activeAlien1 = Level3Alien1;
                activeAlien2 = Level3Alien2;
                activeAlien3 = Level3Alien3;

                dropItem1 = Level3Drop1;
                dropItem2 = Level3Drop2;
                dropItem3 = Level3Drop3;
                break;
            default:
                break;
        }
    }

    void Initialise()
    {
        MoveSpeed = 1;
        LifeSteal = 0;
        AttackSpeed = 1;
        DamageMultiplier = 1;
        hasDouble = false;
        hasExplosion = false;
        HP = 100;
        //OnSceneLoaded();
    }

    // Use this for initialization
    void Start()
    {
        score = 0;
        HP = 100;
    }

    private void Update()
    {
        //prevLevel = currentLevel;
        currLevel = SceneManager.GetActiveScene().buildIndex;
        //if(currLevel == SceneId.LEVEL1 || currLevel == SceneId.LEVEL2 || currLevel == SceneId.LEVEL3 || currLevel == SceneId.STORE)
        //{
            if (scoreText == null)
                scoreText = GameObject.Find("ScoreObject").GetComponent<Text>();
            scoreText.text = "Score: " + score.ToString();

            if (goldText == null)
                goldText = GameObject.Find("GoldObject").GetComponent<Text>();
            goldText.text = "Gold: " + gold.ToString();

            if (PlayerHP == null)
                PlayerHP = GameObject.Find("HP").GetComponent<Text>();
            PlayerHP.text = "HP: " + HP.ToString();
            PlayerHP.color = Color.red;
        //}

        if (HP <= 0)
            EndGame();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(8, 9, -6);
            GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(45, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.Find("Main Camera").transform.position = new Vector3(8, 15, 6);
            GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneId.LOSE);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(SceneId.WIN);
    }

    public void LoadLevelOne()
    {
        Initialise();
        activeAlien1 = Level1Alien1;
        activeAlien2 = Level1Alien2;
        activeAlien3 = Level1Alien3;

        dropItem1 = Level1Drop1;
        dropItem2 = Level1Drop2;
        dropItem3 = Level1Drop3;
        SceneManager.LoadScene(SceneId.LEVEL1);
    }

    public void LoadLevelTwo()
    {
        activeAlien1 = Level2Alien1;
        activeAlien2 = Level2Alien2;
        activeAlien3 = Level2Alien3;

        dropItem1 = Level2Drop1;
        dropItem2 = Level2Drop2;
        dropItem3 = Level2Drop3;

        SceneManager.LoadScene(SceneId.LEVEL2);
    }

    public void LoadLevelThree()
    {
        activeAlien1 = Level3Alien1;
        activeAlien2 = Level3Alien2;
        activeAlien3 = Level3Alien3;

        dropItem1 = Level3Drop1;
        dropItem2 = Level3Drop2;
        dropItem3 = Level3Drop3;

        SceneManager.LoadScene(SceneId.LEVEL3);
    }

    public void LoadNextLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        switch (currentLevel)
        {
            case SceneId.LEVEL1:
                LoadLevelTwo();
                break;
            case SceneId.LEVEL2:
                LoadLevelThree();
                break;
            case SceneId.LEVEL3:
                WinGame();
                break;
            default:
                break;
        }
    }

    public void updateAttackSpeed()
    {
        if(gold >= 150) {
            AttackSpeed *= 1.5f;
            gold -= 150;
        }
        
    }

    public void updateMoveSpeed()
    {
        if (gold >= 150)
        {
            MoveSpeed *= 1.5f;
            gold -= 180;
        }
    }

    public void updateLifeSteal()
    {
        if (gold >= 200)
        {
            LifeSteal += 10.0f;
            gold -= 200;
        }
        
    }

    public void updateDamage()
    {
        if (gold >= 250)
        {
            DamageMultiplier *= 1.5f;
            gold -= 250;
        }
    }
}

