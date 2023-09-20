using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomNum
{
    public static int NumSetStock()
    {
        int randomNum = Random.Range(1, 101);
        int selectedNumber;
        if (randomNum <= 35)
            selectedNumber = 1;
        else if (randomNum <= 60)
            selectedNumber = 2;
        else if (randomNum <= 80)
            selectedNumber = 3;
        else if (randomNum <= 95)
            selectedNumber = 4;
        else
            selectedNumber = 5;
        return selectedNumber;
    }
}
