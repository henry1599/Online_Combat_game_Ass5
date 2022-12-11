using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemManager : MonoBehaviour
{
    public static SpawnItemManager Instance;

    [SerializeField] private SpawnArea spawnArea;
    [SerializeField] private CollectableItem increaseDamageItemPrefab;
    [SerializeField] private CollectableItem increaseHealthItemPrefab;

    [SerializeField] private float firstDelay;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;

    public List<GameObject> HealthItems { get; private set; } = new List<GameObject>();
    public List<GameObject> DamageItems { get; private set; } = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnItem(firstDelay));
    }

    private IEnumerator SpawnItem(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        int ratio = Random.Range(0, 100);
        float x = Random.Range(spawnArea.minX, spawnArea.maxX);
        float z = Random.Range(spawnArea.minZ, spawnArea.maxZ);
        Vector3 position = new Vector3(x, 1, z);
        if (ratio > 50)
        {
            var item = Instantiate(increaseDamageItemPrefab, position, Quaternion.identity);
            item.OnCollected += () => RemoveDamageItem(item.gameObject);
            DamageItems.Add(item.gameObject);
        }
        else
        {
            var item = Instantiate(increaseHealthItemPrefab, position, Quaternion.identity);
            item.OnCollected += () => RemoveHealthItem(item.gameObject);
            HealthItems.Add(item.gameObject);
        }

        float nextDelayTime = Random.Range(minDuration, maxDuration);
        StartCoroutine(SpawnItem(nextDelayTime));
    }

    private void RemoveDamageItem(GameObject item)
    {
        DamageItems.Remove(item);
    }

    private void RemoveHealthItem(GameObject item)
    {
        HealthItems.Remove(item);
    }

    private void OnDrawGizmos()
    {
        Color color = Gizmos.color;
        color.a = 0.2f;
        Gizmos.color = color;
        if (spawnArea != null)
            Gizmos.DrawCube(transform.position + Vector3.up * 0.5f,
                            new Vector3(spawnArea.maxX - spawnArea.minX, 0.5f, spawnArea.maxZ - spawnArea.minZ));
    }
}
