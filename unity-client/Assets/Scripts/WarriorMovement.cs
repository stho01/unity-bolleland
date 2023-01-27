using UnityEngine;

public class WarriorMovement : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    public float jumpForce;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        
        Debug.Log($"Animator = {_animator == null}");
    }

<<<<<<< HEAD
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("Jump");
        }
    }
    
    public void Attack(Move move)
    {
        _animator.SetTrigger("Attack");
    }

    public void Defend(Move move)
    {
        Jump();
    }
    
=======
>>>>>>> 5266a26887f4eabf8660ece9d09b21b5b3235879
    public void Jump()
    {
        _animator.SetTrigger("Jump");
        _rigidbody.AddForce(Vector3.up * jumpForce);
    }
<<<<<<< HEAD
=======

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }
>>>>>>> 5266a26887f4eabf8660ece9d09b21b5b3235879
}
