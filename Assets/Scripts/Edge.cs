using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge {
    public CellCore cell1 { get; private set; }
    public CellCore cell2 { get; private set; }
    public Node vertex1 { get; private set; }
    public Node vertex2 { get; private set; }
    public int id { get; private set; } // the id is unique for each CellCore and allows easy == operator
    private static int totalCellCount = 0; // this is the static int that is used for the id

    public Edge(ref CellCore cell1, ref CellCore cell2, ref Node vertex1, ref Node vertex2) {
        this.cell1 = cell1;
        this.cell2 = cell2;
        this.vertex1 = vertex1;
        this.vertex2 = vertex2;
        id = totalCellCount;
        totalCellCount += 1;
    }

    // la classe edge devrait gérer les transitions
}
