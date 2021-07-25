using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GladiatorState
{
    idle,
    walk,
    attack,
    stagger
}

public class Gladiator : MonoBehaviour
{
    [Header("State Machine")]
    public GladiatorState gladiatorState;

    [Header("Gladiator Stats")]
    public FloatValue maxHealth;
    public float moveSpeed;

    public float health;

    public string gladiatorName;
    public int baseAttack;
    public Vector2 homePosition;


}
