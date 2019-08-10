using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellCore {
    public Vector2 pos { get; private set; } // position of the chunk
    public BiomeGroup biomeGroup { get; private set; }
    public Biome biome { get; private set; }
    [System.NonSerialized]
    public CellCore[] neighbors; // the cellCores with witch boundaries are shared
    [System.NonSerialized]
    public Node[] children;  // the nodes that define the boundaries of the cell
    [System.NonSerialized]
    public Edge[] edges; // the edges that delimit this cellcore ( warning : they have no perticular order )
    // the next 3 variables are used as indexes in the MetaData array
    // ex : MetaData.cores[a, b, c].id = this.id
    public int a { get; private set; }
    public int b { get; private set; }
    public int c { get; private set; }
    public int id { get; private set; } // the id is unique for each CellCore and allows easy == operator
    private static int totalCellCount = 0; // this is the static int that is used for the id

    /// <summary>
    /// public constructor
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    public CellCore(Vector2 pos, int a, int b, int c) {
        this.pos = pos;
        this.a = a;
        this.b = b;
        this.c = c;
        this.neighbors = null;
        this.children = null;
        this.edges = null;
        this.biome = null;
        this.biomeGroup = null;
        id = totalCellCount;
        totalCellCount += 1;
        childrenBuffer = new List<Node>();
        neighborBuffer = new List<CellCore>();
        edgeBuffer = new List<Edge>();
        biome = null;
    }

    /// <summary>
    /// constructor used for the fortune line ( temporary cells )
    /// </summary>
    /// <param name="pos"></param>
    public CellCore(Vector2 pos) {
        this.pos = pos;
    }

    /// <summary>
    /// sets the biome group of the cell
    /// should only be called upon metadata generation, never at runtime
    /// </summary>
    /// <param name="_biomeGroup"></param>
    public void setBiomeGroup(BiomeGroup _biomeGroup) {
        this.biomeGroup = _biomeGroup;
    }

    /// <summary>
    /// sets the biome of the cellcore
    /// should only be called upon metadata generation, never at runtime
    /// </summary>
    /// <param name="_biome"></param>
    public void setBiome(Biome _biome) {
        this.biome = _biome;
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
