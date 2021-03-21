using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManagerScript : MonoBehaviour
{
    bool PlayerWithFlotador = false;
    private GameObject Grid;
    private Transform Agua;
    public GameObject Player;
    private MainCharacter mainCharacterScript;
    private TilemapCollider2D WaterCollider;

    public bool WaterInScene = true;

    public bool PirateHouseInScene = false;
    public GameObject Door;

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
        if (WaterInScene)
        {
            Grid = GameObject.Find("Grid");
            Agua = Grid.transform.Find("Agua");
            WaterCollider = Agua.GetComponent<TilemapCollider2D>();
        }

        actionButton = false;

        Time.timeScale = 1f;
        spawnPlayer();


    }

    private void FixedUpdate()
    {
        if (Player.GetComponent<MainCharacter>().itemInUse == MainCharacter.Trinckets.RubberRing)
        {
            PlayerWithFlotador = true;
        }
        else { PlayerWithFlotador = false; }

        if (mainCharacterScript.GetPirateKey())
        {
            GameObject.Destroy(Door);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (WaterInScene) {
            if (PlayerWithFlotador && WaterCollider != null)
            {
                WaterCollider.enabled = false;
            }
            else { WaterCollider.enabled = true; }
        }

    }

    public bool actionButtonPressed() {
        return actionButton;
    }

    void spawnPlayer() {

        PlayerData data = LoadPlayer();

        if (currentSpawnPoint == defaultValue)
        {
            Player.transform.position = CharaterSpawnPoins[0].position;
            mainCharacterScript = Player.GetComponent<MainCharacter>();
            mainCharacterScript.SetCurrentHealth(4);
            mainCharacterScript.SetCurrentMaxHealth(4);
            mainCharacterScript.SetCurrentCoins(0);
            mainCharacterScript.SetAttackDamage(1);
            mainCharacterScript.SetSword(false);
            mainCharacterScript.SetLamp(false);
            mainCharacterScript.SetRubberRing(false);
            mainCharacterScript.SetPirateKey(false);

        }
        else {
            //Load Player DATA

            if (data == null) {
                Player.transform.position = CharaterSpawnPoins[0].position;
                mainCharacterScript = Player.GetComponent<MainCharacter>();
                mainCharacterScript.SetCurrentHealth(4);
                mainCharacterScript.SetCurrentMaxHealth(4);
                mainCharacterScript.SetCurrentCoins(0);
                mainCharacterScript.SetAttackDamage(1);
                mainCharacterScript.SetSword(false);
                mainCharacterScript.SetLamp(false);
                mainCharacterScript.SetRubberRing(false);
                mainCharacterScript.SetPirateKey(false);

                return;
            }

            Player.transform.position = CharaterSpawnPoins[currentSpawnPoint].position;
            mainCharacterScript = Player.GetComponent<MainCharacter>();

            mainCharacterScript.SetCurrentHealth(data.currentHealth);
            mainCharacterScript.SetCurrentMaxHealth(data.currentMaxHealth);
            mainCharacterScript.SetCurrentCoins(data.coins);
            mainCharacterScript.SetAttackDamage(data.swordAttack);
            mainCharacterScript.SetSword(data.Sword);
            mainCharacterScript.SetLamp(data.Lamp);
            mainCharacterScript.SetRubberRing(data.RubberRing);
            mainCharacterScript.SetPirateKey(data.pirateHouseKey);
        }

        SaveSystem.SavePlayerSystem(data.currentMaxHealth, data.currentMaxHealth, data.Sword, data.Lamp, data.RubberRing, data.coins, data.swordAttack, data.pirateHouseKey, SceneManager.GetActiveScene().name);
    }

    public PlayerData LoadPlayer() {

        return SaveSystem.LoadPlayerSystem();
    }

    public int GetPlayerCoins() {
        return mainCharacterScript.GetCurrentCoins();
    }
    public void SetPlayerState(int state) {
        mainCharacterScript.SetState(state);
    }

    public void setMiniChest(bool miniChest) {
        mainCharacterScript.MiniChestOpened = miniChest;
    }

    public void setBigChest(bool miniChest)
    {
        mainCharacterScript.BigChestOpened = miniChest;
    }

    public void setItemInBigChest(int i) {
        mainCharacterScript.SetItemInChest(i);
    }

    public void BuyingHealthPotion() {
        mainCharacterScript.SetCurrentCoins(mainCharacterScript.GetCurrentCoins() - 3);
        mainCharacterScript.AddHeartConteiner();
    }

    public void BuyingSwordUpgrade() {
        mainCharacterScript.SetCurrentCoins(mainCharacterScript.GetCurrentCoins() - 3);
        mainCharacterScript.SetAttackDamage(mainCharacterScript.GetAttackDamage()+ 1);
    }

    public int GetSwordAttackDamage() {
        return mainCharacterScript.GetAttackDamage();
    }

    public void QuitButtonPressed() {

        SaveSystem.SavePlayerSystem(mainCharacterScript.GetCurrentHealth(), mainCharacterScript.GetCurrentMaxHealth(), mainCharacterScript.GetSword(), mainCharacterScript.GetRubberRing(), mainCharacterScript.GetLamp(), mainCharacterScript.GetCurrentCoins(), mainCharacterScript.GetAttackDamage(), mainCharacterScript.GetPirateKey(), SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MainMenu");
    }
}
