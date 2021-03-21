using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentHealth;
    public int currentMaxHealth;

    public bool Sword;
    public bool RubberRing;
    public bool Lamp;

    public int coins;
    public int swordAttack;

    public bool pirateHouseKey;

    public string scene;

    public PlayerData(int health, int maxHealth, bool sword, bool RubberRing, bool Lamp, int coins, int swordAttack, bool pirateHouseKey, string scene) {
        currentHealth = health;
        currentMaxHealth = maxHealth;

        Sword = sword;
        this.RubberRing = RubberRing;
        this.Lamp = Lamp;

        this.coins = coins;
        this.swordAttack = swordAttack;

        this.pirateHouseKey = pirateHouseKey;

        this.scene = scene;

    }
}
