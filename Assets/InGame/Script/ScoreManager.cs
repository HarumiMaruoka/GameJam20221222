using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static float _score = 0f;

    public static float Score => _score;

    public static void SetScore(float score)
    {
        _score = score;
    }
}
