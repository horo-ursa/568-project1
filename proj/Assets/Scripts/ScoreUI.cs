using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ScoreUI : MonoBehaviour
{
    public GameManager globalObj;
    public Text scoreText;
    // Use this for initialization
    void Start()
    {
        /*GameObject g = GameObject.Find("GameManager");
        globalObj = g.GetComponent<GameManager>();*/
        //scoreText = gameObject.GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(globalObj.score);
        scoreText.text = GameManager.instance.score.ToString();
    }
}