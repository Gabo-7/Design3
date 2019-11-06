using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class Stage

{
    [SerializeField]
    public GameObject parent;

    public int[] respuestaCorrecta;


}


public class StageManager : MonoBehaviour {

    private int currentStage = 0;
    private int currentClip = 0;

    [SerializeField]
    public GameObject WinScreen;

    [SerializeField]
    public GameObject Bolo;

    [SerializeField]
    public AudioSource AS;

    [SerializeField]
    public AudioClip[] Sonidos;

    [SerializeField]
    public GameObject[] Botones;

    [SerializeField]
    public Stage[] Ejercicios;

    



    public void CheckAnswer(int x)
    {

        if(x == Ejercicios[currentStage].respuestaCorrecta[currentClip])
        {
            StartCoroutine(PlaySound(currentClip));
            currentClip++;
            Debug.Log("correct!");
            if (currentClip == Ejercicios[currentStage].respuestaCorrecta.Length)
            {
                WinScreen.SetActive(true);
                Ejercicios[currentStage].parent.SetActive(false);
                Bolo.SetActive(false);
            }
        }
        else
        {
            Debug.Log("incorrect!");
        }

    }

    public void NextChallenge()
    {
        WinScreen.SetActive(false);
        Bolo.SetActive(true);
        currentStage++;
        
        try
        {
            Ejercicios[currentStage].parent.SetActive(true);
            StartCoroutine(PlaySequence());
        }
        catch
        {

        }
    }

    IEnumerator PlaySound(int x)
    {
        yield return null;
        
        AS.clip = Sonidos[x];
        AS.Play();

        while (AS.isPlaying)
        {
            yield return null;
        }
        
    }

    IEnumerator PlaySequence()
    {
        yield return null;

        foreach (int i in Ejercicios[currentStage].respuestaCorrecta)
        {
            AS.clip = Sonidos[i];
            AS.Play();

            while (AS.isPlaying)
            {
                yield return null;
            }
        }
    }

	// Use this for initialization
	void Start () {
        Ejercicios[currentStage].parent.SetActive(true);
        StartCoroutine(PlaySequence());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

