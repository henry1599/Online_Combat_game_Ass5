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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject player in Players)
            {
                player.GetComponent<Player>().IncreaseHealth(20);
            }
        }
    }

    public void AddPlayer(GameObject player)
    {
        Players.Add(player);
    }

    public void RemovePlayer(GameObject player)
    {
        Players.Remove(player);
        StartCoroutine(RespawnPlayer(player));
    }

    public IEnumerator RespawnPlayer(GameObject player)
    {
        yield return new WaitForSeconds(2f);
        player.GetComponent<Player>().Respawn();
        Players.Add(player);
    }
}
