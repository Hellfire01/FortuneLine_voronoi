using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains all of the necessary information to generate the Quad Tree
/// It is only used before the player gets to interact with the map
/// </summary>
public class PreMetaData {
    // obtained after the initial cell placing
    public List<CellCore> allCores;
    public CellCore[][][] cores; // probablement pas utile �tant donn� que toutes les infos seront plac�es dans le quad tree en une seulle fois
    // obtained after the Fortune Line Voronoi
    public List<Node> allNodes;
    public List<Edge> allEdges;
}
