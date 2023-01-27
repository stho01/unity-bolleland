using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    public float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        var v = Input.GetAxis ("Vertical");
        _animator.SetFloat("Speed", v);
        
        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetTrigger("Jump");   
            _rigidbody.AddForce(Vector3.up * jumpForce);
        }
    }
}
