using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TweenAnimations : MonoBehaviour
{ 
    public enum Name
	{
        None, WaveTimer, GameOver
	};

    [Header("Internal Source")]
    public Name nameEnum;

    [Header("Floats")]
    public float tweenTime;
    public float timerTweenTime;

    public void OnEnable()
    {
        if (nameEnum == Name.WaveTimer)
                OpenTimer();

        if(nameEnum == Name.GameOver)
            transform.localScale = Vector3.zero;

    }
	void Start()
    {
        if(nameEnum == Name.None)
            transform.localScale = Vector3.zero;
       
    }

    public void Open()
	{
        transform.LeanScale(Vector3.one, tweenTime);
    }

    public void Close()
	{
        transform.LeanScale(Vector3.zero, tweenTime).setEaseInBack();
	}

    public void OpenTimer()
	{
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, tweenTime).setOnComplete(CloseTimer).setEaseOutExpo().delay = 0.1f;
	}

    public void CloseTimer()
    {
        LeanTween.scale(gameObject, Vector3.zero, timerTweenTime).setEaseInBack().setOnComplete(ObjectOff).setEaseInExpo().delay = 0.5f;
    }

    void ObjectOff()
    {
        if (nameEnum == Name.WaveTimer)
        {
            gameObject.SetActive(false);
        }
    }
}

/*
* Copyright (c) Nhlanhla 'Stud' Langa
*
*/
