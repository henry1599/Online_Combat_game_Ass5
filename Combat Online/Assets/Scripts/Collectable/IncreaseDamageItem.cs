using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamageItem : CollectableItem
{
    [SerializeField] private IncreaseDamageItemData data;

    public override void OnItemCollect(Player player)
    {
        base.OnItemCollect(player);

        player.IncreaseDamage(data.Amount, data.Time);
    }
}
