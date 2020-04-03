using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Start()
    {

        EntityManager entityManager = FindObjectOfType<EntityManager>();
        if(entityManager != null)
        {
            entityManager.OnTowerDestroy += EndGame;
        }
    }

 
    public void EndGame(Alignment alignment)
    {
        switch(alignment)
        {
            case Alignment.Player:
                Debug.Log("LOOOOOOOOOOSE ! GAME OVER !");
                break;
            case Alignment.IA:
                Debug.Log("WIN ! YOU'RE THE BEST");
                break;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
