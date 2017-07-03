using UnityEngine;

// static class to do calculations for exp to level up, stats, damage, etc
public static class AlgorithmManager {

	public static int CalculateEXPRequirement(int level)
    {
        // algorithm: EXP = constant * base ^ sqrt(level)
        /// constant = 6
        /// base = 5

        return 6 * (int)Mathf.Pow(5, Mathf.Sqrt(level));
    }

    /// <summary>
    ///  Damage Calculation for all attacks, including melee and elemental
    /// </summary>
    /// <param name="ATKStat"> If non-elemental, pass in statATK;
    ///                         else pass in elemental dmg stat </param>
    /// <param name="DEFStat"> If non-elemental, pass in statDEF;
    ///                         else pass in elemental resist stat </param>
    /// <returns></returns>
    public static float DamageCalculation(float ATK, float DEF)
    {
        // algorithm: ATK * (algorithm1) - DEF * (algorithm2)
        float tempAlgo = 3f;
        return Mathf.Max(1f, ATK * tempAlgo - DEF * tempAlgo);  // damage cannot be 0 or less
    }

}
