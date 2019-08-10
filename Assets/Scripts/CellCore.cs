using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellCore {
    public Vector2 pos { get; private set; } // position of the chunk
    [System.NonSerialized]
    public CellCore[] neighbors; // the cellCores with witch boundaries are shared
    [System.NonSerialized]
    public Node[] children;  // the nodes that define the boundaries of the cell
    [System.NonSerialized]
    public Edge[] edges; // the edges that delimit this cellcore ( warning : they have no perticular order )
    public int id { get; private set; } // the id is unique for each CellCore and allows easy == operator
    private static int totalCellCount = 0; // this is the static int that is used for the id

    /// <summary>
    /// public constructor
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    public CellCore(Vector2 pos) {
        this.pos = pos;
        this.neighbors = null;
        this.children = null;
        this.edges = null;
        id = totalCellCount;
        totalCellCount += 1;
        childrenBuffer = new List<Node>();
        neighborBuffer = new List<CellCore>();
        edgeBuffer = new List<Edge>();
    }

    // all of the following variables and methods are used only during the diagramm generation
    // the variables are buffer variables and the methods use those buffers

    [System.NonSerialized]
    private List<Node> childrenBuffer;
    [System.NonSerialized]
    private List<CellCore> neighborBuffer;
    [System.NonSerialized]
    private List<Edge> edgeBuffer;

    /// <summary>
    /// adds a child to the buffer list
    /// </summary>
    /// <param name="child"></param>
    public void addChildToBuffer(ref Node child) {
        foreach (Node e in childrenBuffer) {
            if (e.id == child.id) {
                return;
            }
        }
        childrenBuffer.Add(child);
    }

    /// <summary>
    /// adds a neighbor to the buffer list
    /// </summary>
    /// <param name="neighbor"></param>
    public void addNeigborToBuffer(ref CellCore neighbor) {
        foreach (CellCore e in neighborBuffer) {
            if (e.id == neighbor.id) {
                return;
            }
        }
        neighborBuffer.Add(neighbor);
    }

    /// <summary>
    /// adds an edge to the buffer list
    /// </summary>
    /// <param name="edge"></param>
    public void addEdgeToBuffer(Edge edge) {
        foreach (Edge e in edgeBuffer) {
            if (e.id == edge.id) {
                return;
            }
        }
        edgeBuffer.Add(edge);
    }

    /// <summary>
    /// generates the arrays from the lists
    /// </summary>
    public void validateBuffers() {
        neighbors = neighborBuffer.ToArray();
        children = childrenBuffer.ToArray();
        edges = edgeBuffer.ToArray();
    }
}
