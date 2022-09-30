using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{
    public static BossFight instance;
    [SerializeField] private GameObject bossFightArena;
    public TextMeshProUGUI scoreText;

    public GameObject victoryScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        Instantiate(bossFightArena,CharacterController.instance.transform.position,Quaternion.identity);
    }

    private void Update()
    {
        if (IABoss.instance.isDead)
        {
            victoryScreen.SetActive(true);
            scoreText.SetText("Score : " + ListeMonstres.instance.score);
        }
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene("oui");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
