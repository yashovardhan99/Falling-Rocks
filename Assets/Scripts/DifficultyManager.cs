using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DifficultyManager
{
    static float secondsToMaxDifficulty = 60;
    
    public static float getCurrentDifficulty(){
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}