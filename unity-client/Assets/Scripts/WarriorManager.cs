using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveSet
{
    Defend = 0,
    Attack = 1
}

public class WarriorManager : MonoBehaviour
{
    public GameObject warriorA;
    public GameObject warriorB;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Replace with server data. 
        if (Input.GetButtonDown("Jump"))
        {  
            
        }
    }
}