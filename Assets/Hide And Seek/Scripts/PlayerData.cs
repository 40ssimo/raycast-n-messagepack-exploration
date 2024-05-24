using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

[MessagePackObject]
public class PlayerData
{
    [Key(0)]
    public Color bootsColor { get; set; }

    [Key(1)]
    public Color glassesColor { get; set; }

    public PlayerData(Color bootsColor, Color glassesColor)
    {
        this.bootsColor = bootsColor;
        this.glassesColor = glassesColor;
    }

    // Default constructor required for MessagePack
    public PlayerData() { }
}
