using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int redShards;
    public int blueShards;
    public int healthPotions;

    public bool hasEmerald;
    public bool hasStar;
    public bool hasSeal;

    public bool gaveEmerald;
    public bool gaveStar;
    public bool gaveSeal;

    private List<string> clueTitles = new List<string>();
    private List<string> clueBodies = new List<string>();

    void Awake() { Instance = this; }

    public bool AddClue(string title, string body)
    {
        if (clueTitles.Contains(title)) return false;
        clueTitles.Add(title);
        clueBodies.Add(body);
        return true;
    }

    string Status(bool has, bool gave)
    {
        if (gave) return "given to wizard";
        return has ? "collected" : "not found";
    }

    public string BuildSummary()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<b>INVENTORY</b>\n");
        sb.AppendLine("Red shards: " + redShards + "    Blue shards: " + blueShards +
                      "    Health potions: " + healthPotions + "\n");
        sb.AppendLine("<b>Potion items</b>");
        sb.AppendLine("Emerald: " + Status(hasEmerald, gaveEmerald));
        sb.AppendLine("Star: " + Status(hasStar, gaveStar));
        sb.AppendLine("Seal: " + Status(hasSeal, gaveSeal) + "\n");
        sb.AppendLine("<b>Clues</b>");
        if (clueTitles.Count == 0) sb.AppendLine("(none yet)");
        for (int i = 0; i < clueTitles.Count; i++)
            sb.AppendLine("\n<b>" + clueTitles[i] + "</b>\n" + clueBodies[i]);
        return sb.ToString();
    }
}
