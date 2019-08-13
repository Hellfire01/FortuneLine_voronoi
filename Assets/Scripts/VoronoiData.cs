using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains all of the necessary information to generate the Quad Tree
/// It is only used before the player gets to interact with the map
/// </summary>
public class VoronoiData {
    public List<CellCore> cores;
    public List<Node> nodes;
    public List<Edge> edges;
}
