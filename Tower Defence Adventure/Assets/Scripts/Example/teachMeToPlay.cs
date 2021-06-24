using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teachMeToPlay : MonoBehaviour
{

    [Header("Unity Handles")]
    public GameObject[] Instructions;
    public GameObject UI;

    [Header("Integers")]
    public int index;
    public int indexToDisplay = 4;

	private void Awake()
	{
        UI.SetActive(false);
        index = 0;
	}
	private void Update()
    {

        for (int x = 0; x < Instructions.Length; x++)
        {
            if (x == index)
            {
                Instructions[x].SetActive(true);
            }
            else
            {
                Instructions[x].SetActive(false);
                
            }
        }

        if(index == indexToDisplay)
		{
            UI.SetActive(true);
		}

    }
    public void Continue()
    {
        index++;
    }


}
