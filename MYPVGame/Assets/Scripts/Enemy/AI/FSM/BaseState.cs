using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    public bool isComplete {  get; protected set; }
    protected EnemyRadar enemyRadar;
    protected NPCMovement npcMovement;

    public void Setup(EnemyRadar enemyRadar, NPCMovement npcMovement)
    {
        this.enemyRadar = enemyRadar;
        this.npcMovement = npcMovement;
    }


    public abstract void Execute();

    public virtual void EnterState() { }

    public virtual void ExitState() { }

}
