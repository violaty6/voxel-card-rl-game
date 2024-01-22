using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour,IGameService
{
    private void Awake()
    {
        ServiceLocator.Current.Register(this);
    }
    public List<AIPlayerBase> AlliesInDungeon;
    public List<AIPlayerBase> EnemiesInDungeon;
    public List<AIPlayerBase> DeadAlliesInDungeon;
    public List<AIPlayerBase> DeadEnemiesInDungeon;
    public void CardInit(Component sender, object[] data)
    {
        AIPlayerBase ai = sender as AIPlayerBase;
        if (ai != null)
        {
            if((bool) data[0])
            {
                EnemiesInDungeon.Add(ai);
            }
            else
            {
                AlliesInDungeon.Add(ai);
            }
        }
        else
        {
            Debug.LogWarning("The sender is not of type Card.");
        }
    }
}
