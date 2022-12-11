using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealthItem : CollectableItem
{
    [SerializeField] private IncreaseHealthItemData data;

    public override void OnItemCollect(Player player)
    {
        base.OnItemCollect(player);

        player.IncreaseHealth(data.Amount);
    }
}
