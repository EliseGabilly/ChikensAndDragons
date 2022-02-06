using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState {

    private static readonly int CHIKEN_NB = 4;
    private static readonly int COW_NB = 4;
    private static readonly int FOOD_NB = 1;
    private static readonly int WATER_NB = 1;

    public static bool IsGameState { get; set; }
    public static bool IsGameClassique { get => ChickenNb==CHIKEN_NB && CowNb==COW_NB && FoodNb==FOOD_NB && WaterNb==WATER_NB; }

    public static int ChickenNb { get; set; } = CHIKEN_NB;
    public static int CowNb { get; set; } = COW_NB;
    public static int FoodNb { get; set; } = FOOD_NB;
    public static int WaterNb { get; set; } = WATER_NB;
}
