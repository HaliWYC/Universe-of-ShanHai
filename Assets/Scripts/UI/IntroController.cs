using System;
using UnityEngine;
using UnityEngine.Playables;

public class IntroController : MonoBehaviour
{
    public PlayableDirector director;

    private void Awake() 
    {
        director.stopped += OnIntroFinished;
    }

    private void OnIntroFinished(PlayableDirector director)
    {
        GameManager.Instance.OnNewGameEvent();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space)&& director.state == PlayState.Playing)
        {
            director.Stop();
        }
    }
}
