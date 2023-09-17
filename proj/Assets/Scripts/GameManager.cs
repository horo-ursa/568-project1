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
    public int HP = 100;

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
        scoreText = GameObject.Find("ScoreObject").GetComponent<Text>();
        goldText = GameObject.Find("GoldObject").GetComponent<Text>();
    }
    

    public void OnSceneLoaded()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        switch(currentLevel)
        {
            case SceneId.LEVEL1:
                activeAlien1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Alien1.prefab");
                activeAlien2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Alien2.prefab");
                activeAlien3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Alien3.prefab");

                dropItem1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Drop1.prefab");
                dropItem2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Drop2.prefab");
                dropItem3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Drop3.prefab");
                break;
            case SceneId.LEVEL2:
                activeAlien1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Alien1.prefab");
                activeAlien2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Alien2.prefab");
                activeAlien3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Alien3.prefab");

                dropItem1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Drop1.prefab");
                dropItem2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Drop2.prefab");
                dropItem3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Drop3.prefab");
                break;
            case SceneId.LEVEL3:
                activeAlien1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Alien1.prefab");
                activeAlien2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Alien2.prefab");
                activeAlien3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Alien3.prefab");

                dropItem1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Drop1.prefab");
                dropItem2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Drop2.prefab");
                dropItem3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Drop3.prefab");
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
        if(currLevel == SceneId.LEVEL1 || currLevel == SceneId.LEVEL2 || currLevel == SceneId.LEVEL3 || currLevel == SceneId.STORE)
        {
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
        }

        if (HP <= 0)
            EndGame();
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
        activeAlien1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Alien1.prefab");
        activeAlien2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Alien2.prefab");
        activeAlien3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Alien3.prefab");

        dropItem1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Drop1.prefab");
        dropItem2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Drop2.prefab");
        dropItem3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level1/level1Drop3.prefab");
        SceneManager.LoadScene(SceneId.LEVEL1);
    }

    public void LoadLevelTwo()
    {
        activeAlien1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Alien1.prefab");
        activeAlien2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Alien2.prefab");
        activeAlien3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Alien3.prefab");

        dropItem1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Drop1.prefab");
        dropItem2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Drop2.prefab");
        dropItem3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level2/level2Drop3.prefab");

        SceneManager.LoadScene(SceneId.LEVEL2);
    }

    public void LoadLevelThree()
    {
        activeAlien1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Alien1.prefab");
        activeAlien2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Alien2.prefab");
        activeAlien3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Alien3.prefab");

        dropItem1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Drop1.prefab");
        dropItem2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Drop2.prefab");
        dropItem3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Level3/level3Drop3.prefab");

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

