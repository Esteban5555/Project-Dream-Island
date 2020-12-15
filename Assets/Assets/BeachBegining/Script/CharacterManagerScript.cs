using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterManagerScript : MonoBehaviour
{
    bool PlayerWithFlotador = false;
    private GameObject Grid;
    private Transform Agua;
    private GameObject Player;
    private MainCharacter mainCharacterScript;
    private TilemapCollider2D WaterCollider;
    // Start is called before the first frame update
    void Start()
    {
        Grid = GameObject.Find("Grid");
        Agua = Grid.transform.Find("Agua");
        WaterCollider = Agua.GetComponent<TilemapCollider2D>();
        Player = GameObject.Find("MainCharacter");

        mainCharacterScript = Player.GetComponent <MainCharacter>();
    }

    private void FixedUpdate()
    {
        if (mainCharacterScript.RubberRing == true)
        {
            PlayerWithFlotador = true;
        }
        else { PlayerWithFlotador = false; }
        
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
}
