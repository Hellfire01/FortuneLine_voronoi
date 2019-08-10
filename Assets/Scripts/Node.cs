using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node {
    public Vector2 pos; // position of the chunk
    [System.NonSerialized]
    public Node[] sisterNodes; // all of the nodes that have a comon parent with this one
    [System.NonSerialized]
    public CellCore[] parents; // the 2 or 3 parents of the node
    [System.NonSerialized]
    public Edge[] edges; // the edges that start or end at this vertex
    public int id; // valeure unique utilisée pour facilement identifier la classe
    public bool isOutOfBoundary;
    private static int totalNodeCount = 0;

    public Node(Vector2 pos, bool isOutOfBoundary) {
        // the variables a, b and c are set after they are placed in the matadata.nodes array
        this.pos = pos;
        this.sisterNodes = null;
        this.edges = null;
        this.isOutOfBoundary = isOutOfBoundary;
        id = totalNodeCount;
        totalNodeCount += 1;
        sisterBuffer = new List<Node>();
        parentBuffer = new List<CellCore>();
        edgeBuffer = new List<Edge>();
    }

    // all of the following variables and methods are used only during the diagramm generation
    // the variables are buffer variables and the methods use those buffers

    [System.NonSerialized]
    private List<Node> sisterBuffer;
    [System.NonSerialized]
    private List<CellCore> parentBuffer;
    [System.NonSerialized]
    private List<Edge> edgeBuffer;

    /// <summary>
    /// adds a sister to the sister list buffer while making sure there is no duplicates
    /// </summary>
    /// <param name="sister"></param>
    public void addSisterToBuffer(ref Node sister) {
        foreach (Node e in sisterBuffer) {
            if (e.id == sister.id) {
                return;
            }
        }
        sisterBuffer.Add(sister);
    }

    /// <summary>
    /// adds a parent to the parent list buffer while making sure there is no duplicates
    /// </summary>
    /// <param name="core"></param>
    public void addParentToBuffer(ref CellCore core) {
        foreach (CellCore parent in parentBuffer) {
            if (core.id == parent.id) {
                return;
            }
        }
        parentBuffer.Add(core);
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
    /// the buffers are considered valid and are added to the relation arrays
    /// </summary>
    public void validateBuffers() {
        sisterNodes = sisterBuffer.ToArray();
        parents = parentBuffer.ToArray();
        edges = edgeBuffer.ToArray();
    }
}
