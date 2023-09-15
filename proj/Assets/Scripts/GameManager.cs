using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;

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
    }
    

    void OnSceneLoaded()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        switch(currentLevel)
        {
            case SceneId.LEVEL1:
                activeAlien1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/level1Alien1.prefab");
                activeAlien2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/level1Alien2.prefab");
                activeAlien3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/level1Alien3.prefab");
                break;
            case SceneId.LEVEL2:
                activeAlien1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/level2Alien1.prefab");
                activeAlien2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/level2Alien2.prefab");
                activeAlien3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/level2Alien3.prefab");
                break;
            case SceneId.LEVEL3:
                activeAlien1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/level3Alien1.prefab");
                activeAlien2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/level3Alien2.prefab");
                activeAlien3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/level3Alien3.prefab");
                break;
            default:
                break;
        }
    }

    // Use this for initialization
    void Start()
    {
        score = 0;
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
        SceneManager.LoadScene(SceneId.LEVEL1);
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(SceneId.LEVEL2);
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene(SceneId.LEVEL3);
    }

}

