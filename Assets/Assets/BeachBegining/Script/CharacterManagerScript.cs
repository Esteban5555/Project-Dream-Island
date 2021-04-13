using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class CharacterManagerScript : MonoBehaviour
{
    bool PlayerWithFlotador = false;
    private GameObject Grid;
    private Transform Agua;
    public GameObject Player;
    [HideInInspector]
    public MainCharacter mainCharacterScript;
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

    private Transform[] MiniChests;

    private GameObject canvas;

    public bool actionButton;
    // Start is called before the first frame update
    void Start()
    {
        mainCharacterScript = Player.GetComponent<MainCharacter>();
        //Previous Scene
        currentSpawnPoint = PlayerPrefs.GetInt("SpawnPosition", defaultValue);
        if (WaterInScene)
        {
            Grid = GameObject.Find("Grid");
            Agua = Grid.transform.Find("Agua");
            WaterCollider = Agua.GetComponent<TilemapCollider2D>();
        }

        MiniChests = GameObject.Find("MiniChests").GetComponentsInChildren<Transform>();

        actionButton = false;
        canvas = GameObject.Find("Canvas");

        UpdateMiniChests();

        Time.timeScale = 1f;
        spawnPlayer();


    }

    public void SaveChestStates() {
        //Create new list of chests
        List<bool> openes = new List<bool>();

        for (int i = 1; i < MiniChests.Length; i++)
        {
            openes.Add(MiniChests[i].GetComponent<MiniChest>().opened);
            //child is your child transform
        }
        Debug.Log("CHEST STATUS SAVED " + openes.Count);
        Debug.Log("//////////////////////////////////");
        SaveSystem.SaveChestsInScene(SceneManager.GetActiveScene().buildIndex, openes);
    }
    public void UpdateMiniChests()
    {
        MinichestStatus chestData = SaveSystem.LoadChestsInScene();

        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        Debug.Log(chestData.ChestStatusInGame.Count);

        if (SceneManager.GetActiveScene().buildIndex > chestData.ChestStatusInGame.Count - 1 || chestData.ChestStatusInGame[SceneManager.GetActiveScene().buildIndex] == null)
        {
            Debug.Log("Creating ChestSave");
            //Create new list of chests
            List<bool> openes = new List<bool>();
            for (int i = 1; i < MiniChests.Length; i++) {
                openes.Add(MiniChests[i].GetComponent<MiniChest>().opened);
            }

            SaveSystem.SaveChestsInScene(SceneManager.GetActiveScene().buildIndex, openes);
        }
        else {
            Debug.Log("Loading Chest save");
            List<bool> openes = chestData.ChestStatusInGame[SceneManager.GetActiveScene().buildIndex].OpenedStatus;
            for(int i = 1; i < MiniChests.Length; i++)
            {
                MiniChests[i].GetComponent<MiniChest>().opened = openes[i-1];
                //child is your child transform
            }
        }
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
            if(Door != null) GameObject.Destroy(Door);
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

        SaveSystem.SavePlayerSystem(data.currentMaxHealth, data.currentMaxHealth, data.Sword, data.RubberRing, data.Lamp, data.coins, data.swordAttack, data.pirateHouseKey, SceneManager.GetActiveScene().name);
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
        SaveChestStates();
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayerDead() {
        canvas.GetComponent<PauseMenu>().GameOverMenu();
    }

    public void SavingChests() {
        //Create new list of chests
        List<bool> openes = new List<bool>();
        foreach (Transform child in transform)
        {
            openes.Add(child.GetComponent<MiniChest>().opened);
            //child is your child transform
        }

        SaveSystem.SaveChestsInScene(SceneManager.GetActiveScene().buildIndex, openes);
    }
}
