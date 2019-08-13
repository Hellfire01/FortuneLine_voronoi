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
        List<Vector2> cellPos = new List<Vector2>();
        List<CellCore> cells = new List<CellCore>();
        Debug.Log("initializing");
        while (i < count) {
            double x = prng.Next(0, mapSize);
            double y = prng.Next(0, mapSize);
            xVald[i] = x;
            yVald[i] = y;
            xValf[i] = (float)x;
            yValf[i] = (float)y;
            Vector2 buff = new Vector2((float)x, (float)y);
            cellPos.Add(buff);
            cells.Add(new CellCore(buff));
            i += 1;
        }
        Debug.Log("initialized");
        VoronoiData vd1 = GetVoronoï.getVoronoi(xVald, yVald, mapSize);
        Debug.Log("Voronoi 1 done");
        VoronoiData vd2 = GetVoronoï.getVoronoi(xValf, yValf, mapSize);
        Debug.Log("Voronoi 2 done");
        VoronoiData vd3 = GetVoronoï.getVoronoi(cellPos, mapSize);
        Debug.Log("Voronoi 3 done");
        VoronoiData vd4 = GetVoronoï.getVoronoi(cells, mapSize);
        Debug.Log("Voronoi 4 done");
    }
}
