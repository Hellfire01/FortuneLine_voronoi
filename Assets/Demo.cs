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
        double[] xVald = new double[count];
        double[] yVald = new double[count];
        float[] xValf = new float[count];
        float[] yValf = new float[count];
        List<CellCore> cells = new List<CellCore>();
        while (i < count) {
            double x = prng.Next(0, mapSize);
            double y = prng.Next(0, mapSize);
            xVald[i] = x;
            yVald[i] = y;
            xValf[i] = (float)x;
            yValf[i] = (float)y;
            cells.Add(new CellCore(new Vector2((float)x, (float)y)));
        }
    }
}
