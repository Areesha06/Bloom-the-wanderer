using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public int redShards;
    public int blueShards;
    public bool hasEmerald = false;

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

    public string BuildSummary()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<b>INVENTORY</b>\n");
        sb.AppendLine("Red shards: " + redShards + "    Blue shards: " + blueShards + "\n");
        if (hasEmerald){
            sb.AppendLine("Emerald: Collected");}
        else{
            sb.AppendLine("Emerald: Not collected");}
        sb.AppendLine("<b>Clues</b>");
        if (clueTitles.Count == 0) sb.AppendLine("(none yet)");
        for (int i = 0; i < clueTitles.Count; i++)
            sb.AppendLine("\n<b>" + clueTitles[i] + "</b>\n" + clueBodies[i]);
        return sb.ToString();
    }
}