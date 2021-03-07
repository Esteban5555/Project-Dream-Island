using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionBetweedScenes : MonoBehaviour
{
    public int nextSpawnPointIndicator;
    public string nextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCharacter") {
            PlayerPrefs.SetInt("SpawnPosition", nextSpawnPointIndicator);
            MainCharacter script = collision.transform.GetComponent<MainCharacter>();
            SaveSystem.SavePlayerSystem(script.GetCurrentHealth(), script.GetCurrentMaxHealth(), script.GetSword(), script.GetRubberRing(), script.GetLamp(), script.GetCurrentCoins(), script.GetAttackDamage(), SceneManager.GetActiveScene().name) ;
            SceneManager.LoadScene(nextScene);
        }
    }
}
