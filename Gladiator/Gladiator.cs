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
    public IntValue maxHealth;
    public FloatValue InitmoveSpeed;
    public IntValue DamageIntValue;
    public FloatValue ProjectileSpeed;
    public FloatValue WeaponSpeed;
    public IntValue Level_IntValue;
    public IntValue UpgradeLevel_IntValue;
    public BoolValue Touch_BoolValue;
    public BoolValue Alive_BoolValue;
    public IntValue TeamSite_IntValue;

    protected int A_Team = 1;
    protected int B_Team = 2;
    protected int A_Team_Layer = 11;
    protected int B_Team_Layer = 12;

    public float moveSpeed;
    public int health;
    public int Level;
    public string gladiatorName;
    public int baseAttack;
    public float AttackSpeed;
    public float ProjectileSpeed_base;
    public Vector2 homePosition;

    public Rigidbody2D myRigidbody;
    public Animator anim;

    [Header("Health Bar")]
    public HealthBar healthBar;

    private void Start()
    {
        Debug.Log("call?");
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
        anim.SetBool("moving", true);
    }

    public virtual void changeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }
}
