using System;
using UnityEngine;

[Serializable]
public class WarriorStats
{
    public int baseHealth;
    public int stamina;
    public int agility;
    public int defence;
}

public class WarriorBehaviour : MonoBehaviour
{
    public WarriorStats stats = new();
    public float jumpForce;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private int _currentHealth;
    
    public int TotalHealth => stats.baseHealth * stats.stamina;
    public int HealthPercentage => (int)((_currentHealth / (float) TotalHealth) * 100f);
    public bool IsDead => HealthPercentage <= 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _currentHealth = TotalHealth;
        Debug.Log($"Animator = {_animator == null}");
    }

    void Update()
    {
        var isDeadBeforeUpdate = IsDead;

        if (Input.GetKeyDown(KeyCode.E))
            _animator.SetTrigger("Defend");
        if (Input.GetKeyDown(KeyCode.R))
            _animator.SetTrigger("Crouch");
        
        if (IsDead && !isDeadBeforeUpdate)
            _animator.SetTrigger("Died");
    }
    
    public void Attack(Move move)
    {
        _animator.SetTrigger("Attack");
    }

    public void Defend(Move move)
    {
        switch (move)
        {
            case Move.Mid:
                _animator.SetTrigger("Defend");
                break;
            case Move.Low:
                _animator.SetTrigger("Crouch");
                break;
        }
        
        // Crouch();
    }
    
    public void Jump()
    {
        _animator.SetTrigger("Jump");
        _rigidbody.AddForce(Vector3.up * jumpForce);
    }
}
