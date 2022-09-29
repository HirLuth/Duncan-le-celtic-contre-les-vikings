using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLooper : MonoBehaviour
{
    [SerializeField] private Transform floor;
    [SerializeField] private SpriteRenderer self;
    private float _floorHeight;
    private float _floorWidth;
    private Vector3 _currentPos;
    private Vector3 _lastPos;

    void Start()
    {
        _floorHeight = 40;
        _floorWidth = 40;
        _lastPos = CharacterController.instance.transform.position;
    }
    
    private void Update()
    {
        _currentPos = CharacterController.instance.transform.position;
        CheckUp();
        CheckRight();
        CheckDown();
        CheckLeft();
    }

    private void CheckUp()
    {
        if (_currentPos.y>_lastPos.y+_floorHeight/2)
        {
            floor.position = new Vector3(_lastPos.x,_lastPos.y+_floorWidth,0);
            _lastPos = floor.position;
        }
    }
    private void CheckRight()
    {
        if (_currentPos.x>_lastPos.x+_floorWidth/2)
        {
            floor.position = new Vector3(_lastPos.x+_floorWidth,_lastPos.y,0);
            _lastPos = floor.position;
        }
    }
    private void CheckDown()
    {
        if (_currentPos.y<_lastPos.y-_floorHeight/2)
        {
            floor.position = new Vector3(_lastPos.x,_lastPos.y-_floorWidth,0);
            _lastPos = floor.position;
        }
    }
    private void CheckLeft()
    {
        if (_currentPos.x<_lastPos.x-_floorWidth/2)
        {
            floor.position = new Vector3(_lastPos.x-_floorWidth,_lastPos.y,0);
            _lastPos = floor.position;
        }
    }
}
