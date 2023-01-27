using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Move
{
    High = 0,
    Mid = 1,
    Low = 2
}

public class WarriorManager : MonoBehaviour
{
    public GameObject warriorA;
    public GameObject warriorB;
    private WarriorMovement _player1Movement;
    private WarriorMovement _player2Movement;
    private GameObject _player1;
    private GameObject _player2;
    private readonly Queue<Frame> _frames = new();
    
    private Frame _currentFrame;
    private int _frameCounter = 0;
    private float _currentFrameTime = 0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _player1 = Instantiate(warriorA, new Vector3(0,0,1f), new Quaternion(0f,180f,0f, 1), transform);
        _player1Movement = _player1.GetComponent<WarriorMovement>();
        _player2 = Instantiate(warriorB, new Vector3(0,0,-1f), Quaternion.identity, transform);
        _player2Movement = _player2.GetComponent<WarriorMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Replace with server data. 
        if (Input.GetKeyDown(KeyCode.Q) && !_frames.Any())
        {
            for (var i = 0; i < 3; i++)
            {
                _frames.Enqueue(new Frame(
                    _player1Movement,
                    (Move)Random.Range(0, 2),
                    _player2Movement,
                    (Move)Random.Range(0, 2)
                ));
                
                _frames.Enqueue(new Frame(
                    _player2Movement,
                    (Move)Random.Range(0, 2),
                    _player1Movement,
                    (Move)Random.Range(0, 2)
                ));
            }
        }

        if (_frames.Any())
        {
            if (_currentFrameTime >= 2f)
            {
                _currentFrameTime = 0f;
                _currentFrame = _frames.Dequeue();
                _currentFrame.Execute();
            }

            _currentFrameTime += Time.deltaTime;
        }
        else
        {
            _currentFrameTime = 0f;
        }
    }


    class Frame
    {
        private readonly WarriorMovement _attackingWarrior;
        private readonly Move _attack;
        private readonly WarriorMovement _defendingWarrior;
        private readonly Move _defend;

        public Frame(
            WarriorMovement attackingWarrior,
            Move attack,
            WarriorMovement defendingWarrior,
            Move defend)
        {
            _attackingWarrior = attackingWarrior;
            _attack = attack;
            _defendingWarrior = defendingWarrior;
            _defend = defend;
        }

        public void Execute()
        {
            _attackingWarrior.Attack(_attack);
            _defendingWarrior.Defend(_defend);
            
            // todo: calculate dmg taken  
        } 
    }
}