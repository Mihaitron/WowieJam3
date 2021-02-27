using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageState
{
    NONE,
    HALF,
    FULL
}

public class Heart : MonoBehaviour
{
    public DamageState state = DamageState.FULL;
}
