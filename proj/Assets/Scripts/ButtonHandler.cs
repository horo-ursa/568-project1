using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonHandler : MonoBehaviour
{
    
    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        GameManager.instance.score = 0;
        GameManager.instance.gold = 0;
        GameManager.instance.LoadLevelOne();
    }

    public void updateAttackSpeed()
    {
        if (GameManager.instance.gold >= 150)
        {
            GameManager.instance.AttackSpeed *= 1.5f;
            GameManager.instance.gold -= 150;
        }

    }

    public void updateMoveSpeed()
    {
        if (GameManager.instance.gold >= 200)
        {
            GameManager.instance.MoveSpeed *= 1.5f;
            GameManager.instance.gold -= 200;
        }
    }

    public void updateLifeSteal()
    {
        if (GameManager.instance.gold >= 250)
        {
            GameManager.instance.LifeSteal += 2.0f;
            GameManager.instance.gold -= 250;
        }

    }

    public void updateDamage()
    {
        if (GameManager.instance.gold >= 300)
        {
            GameManager.instance.DamageMultiplier *= 1.5f;
            GameManager.instance.gold -= 300;
        }
    }

    public void returnBacktoGame()
    {
        if(GameManager.instance.prevLevel == SceneId.LEVEL1)
        {
            GameManager.instance.LoadLevelTwo();
        }
        else if(GameManager.instance.prevLevel == SceneId.LEVEL2)
        {
            GameManager.instance.LoadLevelThree();
        }
        
    }
}
