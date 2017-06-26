using UnityEngine;
using System.Collections;

// static class to do calculations for exp to level up, stats, damage, etc
public static class StatsAlgorithmManager {

	public static int CalculateEXPRequirement(int level)
    {
        // algorithm: EXP = constant * 4 ^ x

        return 6 * (int)Mathf.Pow(4, level);
    }

}
