using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Demo : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        System.Random prng = new System.Random();
        int i = 0;
        int count = 8;
        int mapSize = 100;
        double[] xVal = new double[count];
        double[] yVal = new double[count];
        List<CellCore> cells = new List<CellCore>();
        while (i < count) {
            float x = prng.Next(0, mapSize);
            float y = prng.Next(0, mapSize);
            xVal[i] = x;
            yVal[i] = y;
            cells.Add(new CellCore(new Vector2(x, y)));
        }
    }
}
