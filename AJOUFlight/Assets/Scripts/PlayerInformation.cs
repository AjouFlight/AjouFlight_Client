using System.Collections.Generic;

public static class PlayerInformation
{
    public static string token { get; set; } = "";
    public static string playerID { get; set; } = "Custom";
    public static int clearedStage { get; set; } = 0;
    public static double money { get; set; } = 0;
    public static int ranking { get; set; } = 10;
    public static int score { get; set; } = 0;

    public static int[] canSelectFlight { get; set; } = { 1, 0, 0, 0 };
    public static int selectedFlight { get; set; } = 0;
    public static int selectedSkin { get; set; } = -1;

    public static int currentStage { get; set; } = 1;

    public static List<Dictionary<string, int>> tenUserlist { get; set; } = new List<Dictionary<string, int>>();
}
