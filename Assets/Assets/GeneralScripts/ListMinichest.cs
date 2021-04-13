using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ListMinichest
{
    public int id;

    public List<bool> OpenedStatus;

    public ListMinichest(int id, List<bool> OpenedStatus) {
        this.id = id;
        this.OpenedStatus = OpenedStatus;
    }
}

[System.Serializable]
public class MinichestStatus 
{
    public List<ListMinichest> ChestStatusInGame;
    public MinichestStatus() {
        ChestStatusInGame = new List<ListMinichest>();
    }
}
