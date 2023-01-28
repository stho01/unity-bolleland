using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WarriorManager : MonoBehaviour
{
    public WarriorBehaviour player1;
    public WarriorBehaviour player2;
    public GameObject hud;
    
    private GameObject _hudInstance;
    private ProgressBar _player1ProgressBar;
    private ProgressBar _player2ProgressBar;
    
    private readonly Queue<Frame> _frames = new();
    private Frame _currentFrame;
    private float _currentFrameTime = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        player1 = Instantiate(player1, new Vector3(0,0,1f), new Quaternion(0f,180f,0f, 1), transform);
        player2 = Instantiate(player2, new Vector3(0,0,-1f), Quaternion.identity, transform);
        _hudInstance = Instantiate(hud, transform);
        var progressBars = _hudInstance.GetComponentsInChildren<ProgressBar>();
        _player1ProgressBar = progressBars.First(x => x.name == "Player1Health");
        _player2ProgressBar = progressBars.First(x => x.name == "Player2Health");
    }

    // Update is called once per frame
    void Update()
    {
        _player1ProgressBar.BarValue = player1.HealthPercentage;
        _player2ProgressBar.BarValue = player2.HealthPercentage;
        
        // TODO: Replace with server data. 
        if (Input.GetKeyDown(KeyCode.Q) && !_frames.Any())
        {
            for (var i = 0; i < 3; i++)
            {
                _frames.Enqueue(new Frame(
                    player1,
                    (Move)Random.Range(0, 3),
                    player2,
                    (Move)Random.Range(0, 3)
                ));
                
                _frames.Enqueue(new Frame(
                    player2,
                    (Move)Random.Range(0, 3),
                    player1,
                    (Move)Random.Range(0, 3)
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
        private readonly WarriorBehaviour _attackingWarrior;
        private readonly Move _attack;
        private readonly WarriorBehaviour _defendingWarrior;
        private readonly Move _defend;

        public Frame(
            WarriorBehaviour attackingWarrior,
            Move attack,
            WarriorBehaviour defendingWarrior,
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