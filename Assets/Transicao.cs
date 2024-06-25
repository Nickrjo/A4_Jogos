using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transicao : MonoBehaviour
{
    public int LevelIndex;
    public Animator transition;
    public float tempTransicao;

    private void Start()
    {
        transition = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        
        if (collision.tag == "Player")
        {
            Debug.Log("Player entrou na transição");
            StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }
    private IEnumerator LoadNextLevel(int LevelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(tempTransicao);
        Debug.Log("Mudando para cena " + LevelIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}