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
    public int Level;
    public string gladiatorName;
    public float baseAttack;
    public Vector2 homePosition;


}
