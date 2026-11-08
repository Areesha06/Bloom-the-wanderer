using UnityEngine;
using System.Collections.Generic;

public class Wizard : Interactable
{
    public int blueShardsReward = 2;

    private bool met;
    private bool rewarded;

    void Reset() { prompt = "Press E to talk"; }

    public override void Interact()
    {
        Inventory inv = Inventory.Instance;
        GameUI ui = GameUI.Instance;

        // First meeting
        if (!met)
        {
            met = true;
            ui.ShowClue("The Wizard",
                "Ah, young Prince! The Evil Ruler stole the shards, but I saved two blue ones.\n\n" +
                "Help me finish my potion and they are yours. I need three things:\n" +
                "a missing emerald, a captured star, and a royal seal.\n\n" +
                "Each one is hidden behind one of the three doors. Bring them to me!");
            return;
        }

        // Already finished
        if (rewarded)
        {
            ui.ShowClue("The Wizard",
                "Thank you again, Prince! Drink the potion wisely. Good luck against the Evil Ruler!");
            return;
        }

        // Take whatever the player is carrying
        List<string> given = new List<string>();
        if (inv.hasEmerald && !inv.gaveEmerald) { inv.hasEmerald = false; inv.gaveEmerald = true; given.Add("the emerald"); }
        if (inv.hasStar && !inv.gaveStar)       { inv.hasStar = false;    inv.gaveStar = true;    given.Add("the star"); }
        if (inv.hasSeal && !inv.gaveSeal)       { inv.hasSeal = false;    inv.gaveSeal = true;    given.Add("the seal"); }

        // Everything delivered: reward
        if (inv.gaveEmerald && inv.gaveStar && inv.gaveSeal)
        {
            rewarded = true;
            inv.blueShards += blueShardsReward;
            inv.healthPotions += 1;
            ui.ShowClue("The Wizard",
                "You found them all! My potion is finished. Thank you, brave Prince.\n\n" +
                "Here are the " + blueShardsReward + " blue shards, and a health potion as a bonus.\n" +
                "<size=70%>(Press H to drink the potion)</size>");
            ui.ShowMessage("Received " + blueShardsReward + " blue shards and a health potion!");
            return;
        }

        // Partial delivery or nothing yet
        string text = "";
        if (given.Count > 0)
            text = "Wonderful! Thank you for " + string.Join(" and ", given) + ".\n\n";
        text += "I still need: " + MissingList(inv) + ".";
        ui.ShowClue("The Wizard", text);
    }

    string MissingList(Inventory inv)
    {
        List<string> missing = new List<string>();
        if (!inv.gaveEmerald) missing.Add("the missing emerald");
        if (!inv.gaveStar)    missing.Add("a captured star");
        if (!inv.gaveSeal)    missing.Add("the royal seal");
        return string.Join(", ", missing);
    }
}