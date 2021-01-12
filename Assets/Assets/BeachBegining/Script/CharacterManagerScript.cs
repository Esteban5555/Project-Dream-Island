using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class CharacterManagerScript : MonoBehaviour
{
    bool PlayerWithFlotador = false;
    private GameObject Grid;
    private Transform Agua;
    public GameObject Player;
    private MainCharacter mainCharacterScript;
    private TilemapCollider2D WaterCollider;

    private int defaultValue = -1;

    int currentSpawnPoint;

    //PlayerData

    int CurrentHealth;
    int CurrentMaxHealth;

    bool Sword;
    bool Lamp;
    bool RubberRing;

    int Coins;

    public Transform[] CharaterSpawnPoins;

    public bool actionButton;
    // Start is called before the first frame update
    void Start()
    {
        //Previous Scene
        currentSpawnPoint = PlayerPrefs.GetInt("SpawnPosition", defaultValue);
        Debug.Log(currentSpawnPoint);

        Grid = GameObject.Find("Grid");
        Agua = Grid.transform.Find("Agua");
        WaterCollider = Agua.GetComponent<TilemapCollider2D>();

        actionButton = false;

        spawnPlayer();


    }

    private void FixedUpdate()
    {
        if (Player.GetComponent<MainCharacter>().itemInUse == MainCharacter.Trinckets.RubberRing)
        {
            PlayerWithFlotador = true;
        }
        else { PlayerWithFlotador = false; }

        actionButton = mainCharacterScript.actionButton;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerWithFlotador && WaterCollider != null)
        {
            WaterCollider.enabled = false;
        }
        else { WaterCollider.enabled = true; }
    }

    public bool actionButtonPressed() {
        return actionButton;
    }

    void spawnPlayer() {
        if (currentSpawnPoint == defaultValue)
        {
            Player.transform.position = CharaterSpawnPoins[0].position;
            mainCharacterScript = Player.GetComponent<MainCharacter>();
            mainCharacterScript.SetCurrentHealth(4);
            mainCharacterScript.SetCurrentMaxHealth(4);
            mainCharacterScript.SetCurrentCoins(0);
            mainCharacterScript.SetSword(true);
            mainCharacterScript.SetLamp(true);
            mainCharacterScript.SetRubberRing(true);

        }
        else {
            //Load Player DATA
            Player.transform.position = CharaterSpawnPoins[currentSpawnPoint].position;
            mainCharacterScript = Player.GetComponent<MainCharacter>();
            mainCharacterScript.SetCurrentHealth(4);
            mainCharacterScript.SetCurrentMaxHealth(4);
            mainCharacterScript.SetCurrentCoins(0);
            mainCharacterScript.SetSword(true);
            mainCharacterScript.SetLamp(true);
            mainCharacterScript.SetRubberRing(true);
        }
    }
}
