using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageScript : EnemyScript
{
    protected override void BehaviourSetup()
    {
        agent.SetDestination(targetPos.position);
    }

    protected override void DeadBehaviour()
    {
        GameManager.Instance.PlayerHit(1);
        GameManager.Instance.HostageKilled(transform.position + Vector3.up * 0.4f);
    }
}
