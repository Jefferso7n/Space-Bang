using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    #region Declarations
    int damage = 0, kills = 0;
    string timer;

    static ScoreKeeper instance;
    #endregion

    #region Singleton
    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region Damage
    public int GetDamage()
    {
        return damage;
    }

    public void ModifyDamage(int value)
    {
        damage += value;
        Mathf.Clamp(damage, 0, int.MaxValue);
    }
    #endregion

    #region Kills
    public int GetKills()
    {
        return kills;
    }

    public void ModifyKills()
    {
        kills += 1;
        Mathf.Clamp(kills, 0, int.MaxValue);
    }
    #endregion

    #region Timer
    public string GetTimer()
    {
        return timer;
    }

    public void ModifyTimer(string _timer)
    {
        timer = _timer;
    }
    #endregion

    #region Reset Score
    public void ResetScore()
    {
        damage = 0;
        kills = 0;
        timer = "";
    }
    #endregion
}
