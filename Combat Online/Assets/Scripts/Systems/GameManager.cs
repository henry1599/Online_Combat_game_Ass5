using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> Players;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPlayer(GameObject player)
    {
        Players.Add(player);
    }

    public void RemovePlayer(GameObject player)
    {
        Players.Remove(player);
    }
}
