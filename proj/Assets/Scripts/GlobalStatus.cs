using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneId
{
    public const int OPENING = 0;
    public const int LEVEL1 = 1;
    public const int LEVEL2 = 2;
    public const int LEVEL3 = 3;
    public const int WIN = 4;
    public const int LOSE = 5;
    public const int STORE = 6;
}

public enum AttackType
{
    DOUBLE,
    EXPLOSION,
    //LASER
}

public class PlayerAbility
{
    public const float MoveSpeed = 1;
    public const float LifeSteal = 0;
    public const float AttackSpeed = 1;
    public const float DamageMultiplier = 1;
}