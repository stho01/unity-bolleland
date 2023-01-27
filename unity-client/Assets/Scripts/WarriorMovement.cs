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

    public void Jump()
    {
        _animator.SetTrigger("Jump");
        _rigidbody.AddForce(Vector3.up * jumpForce);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }
}
