using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCamManager : MonoBehaviour
{
    [SerializeField] string[] _camNames;
    int _currentCamIndex = 0;

    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchState(int indexChange)
    {
        //Change the current camera index based on the direction of the camera change
        _currentCamIndex += indexChange;

        //Set the index to the min or max based on if it goes above the max or below to starting index

        if(_currentCamIndex < 0)
        {
            _currentCamIndex = _camNames.Length - 1;
        } else if(_currentCamIndex >= _camNames.Length)
        {
            _currentCamIndex = 0;
        }

        //Play the animation state based on the name pulled using the camera index
        _animator.Play(_camNames[_currentCamIndex]);

        //When playing the animation state in the animator, the cinemachine will blend to the camera assigned to that state (E.g. if the state played is "Main", the main camera will blend to the cinemachine camera called "Main" from the current camera. How it blends is based on the defined blend settings)
    }
}
