using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    public bool isComplete {  get; protected set; }
    protected EnemyRadar enemyRadar;
    protected NPCMovement npcMovement;
    protected Health health;
    protected Enemy enemy;
    protected Formation formation { get; private set; }

    public void Setup(EnemyRadar enemyRadar, NPCMovement npcMovement, Health health, Formation formation, Enemy enemy)
    {
        this.enemyRadar = enemyRadar;
        this.npcMovement = npcMovement;
        this.health = health;
        this.formation = formation;
        this.enemy = enemy;
    }


    public abstract void Execute();

    public virtual void EnterState() { }

    public virtual void ExitState() { }

}
