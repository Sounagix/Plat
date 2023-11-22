using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelienCast : Brain
{

    private void Start()
    {
        SetUp();
        SetStopDistance();
    }

    protected override void SetStopDistance()
    {
        agent.stoppingDistance = attack.GetRangeAttack();
    }
}
