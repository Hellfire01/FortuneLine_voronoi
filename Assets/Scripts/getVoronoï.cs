using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using FortuneLine;

public static class getVoronoï {
    /// <summary>
    /// takes in the cellCore list and translates it into the class that is used for the fortune Voronoi
    /// </summary>
    /// <param name="cells"></param>
    /// <returns></returns>
    private static Site[] translate_cellCore_data(List<CellCore> cells) {
        Site[] ret = new Site[cells.Count];
        for (int i = 0; i < cells.Count; i++) {
            ret[i] = new Site(true);
            ret[i].assigned_cellCore = cells[i];
            ret[i].coord.setPoint(cells[i].pos.x, cells[i].pos.y);
        }
        return ret;
    }

    /// <summary>
    /// either finds and returns te correct node or creates one, adds it to the dictionnary, adds it to the meta data and then returns it
    /// </summary>
    /// <param name="vertex_dictionnary"></param>
    /// <param name="pos"></param>
    /// <param name="metaData"></param>
    /// <param name="mapSize"></param>
    /// <returns></returns>
    private static Node get_or_assign_node_in_dictionnary(ref Dictionary<Vector2, Node> vertex_dictionnary, Vector2 pos, ref VoronoiData metaData, int mapSize) {
        Node buffer = null;
        try {
            buffer = vertex_dictionnary[pos];
        } catch (KeyNotFoundException) {
            bool is_out_of_bounds = false;
            if (pos.x < 0 || pos.x > mapSize || pos.y < 0 || pos.y > mapSize) {
                is_out_of_bounds = true;
            }
            buffer = new Node(pos, is_out_of_bounds);
            metaData.allNodes.Add(buffer);
            vertex_dictionnary.Add(pos, buffer);
        }
        return buffer;
    }

    /// <summary>
    /// assigns the sister / parent / neighbor relations
    /// </summary>
    /// <param name="cell1"></param>
    /// <param name="cell2"></param>
    /// <param name="vertex1"></param>
    /// <param name="vertex2"></param>
    /// <param name="metaData"></param>
    private static void compute_relations_vertex_and_cellcores(ref CellCore cell1, ref CellCore cell2, ref Node vertex1, ref Node vertex2, ref VoronoiData metaData) {
        cell1.addNeigborToBuffer(ref cell2);
        cell1.addChildToBuffer(ref vertex1);
        cell1.addChildToBuffer(ref vertex2);
        cell2.addNeigborToBuffer(ref cell1);
        cell2.addChildToBuffer(ref vertex1);
        cell2.addChildToBuffer(ref vertex2);
        vertex1.addSisterToBuffer(ref vertex2);
        vertex1.addParentToBuffer(ref cell1);
        vertex1.addParentToBuffer(ref cell2);
        vertex2.addSisterToBuffer(ref vertex1);
        vertex2.addParentToBuffer(ref cell1);
        vertex2.addParentToBuffer(ref cell2);
        Edge edge = new Edge(ref cell1, ref cell2, ref vertex1, ref vertex2);
        cell1.addEdgeToBuffer(edge);
        cell2.addEdgeToBuffer(edge);
        vertex1.addEdgeToBuffer(edge);
        vertex2.addEdgeToBuffer(edge);
        metaData.allEdges.Add(edge);
    }

    /// <summary>
    /// extracts all of the relevant information from the graph edge list in order to be able to use it
    /// </summary>
    /// <param name="to_translate"></param>
    /// <param name="metaData"></param>
    /// <param name="mapSize"></param>
    private static void extract_data_from_graph_edge_list(List<GraphEdge> to_translate, ref VoronoiData metaData, int mapSize) {
        Dictionary<Vector2, Node> vertex_dictionnary = new Dictionary<Vector2, Node>();
        foreach (GraphEdge edge in to_translate) {
            Vector2 pos1 = new Vector2((float)edge.x1, (float)edge.y1);
            Vector2 pos2 = new Vector2((float)edge.x2, (float)edge.y2);
            Node buffer1 = get_or_assign_node_in_dictionnary(ref vertex_dictionnary, pos1, ref metaData, mapSize);
            Node buffer2 = get_or_assign_node_in_dictionnary(ref vertex_dictionnary, pos2, ref metaData, mapSize);
            compute_relations_vertex_and_cellcores(ref edge.site_class_1.assigned_cellCore, ref edge.site_class_2.assigned_cellCore, ref buffer1, ref buffer2, ref metaData);
        }
    }

    /// <summary>
    /// validates all of the buffers of the vertices and cellcores
    /// </summary>
    /// <param name="metaData"></param>
    private static void validate_all_buffers(ref VoronoiData metaData) {
        foreach (Node node in metaData.allNodes) {
            node.validateBuffers();
        }
        foreach (CellCore cell in metaData.allCores) {
            cell.validateBuffers();
        }
    }

    /// <summary>
    /// regroups all of the APIs in order to execute the rest of the programm
    /// </summary>
    /// <param name="metaData"></param>
    /// <param name="mapSize"></param>
    private static void getVoronoi(ref VoronoiData metaData, int mapSize) {
        Site[] translated_sites = translate_cellCore_data(metaData.allCores);
        double[] xVal = new double[translated_sites.Length];
        double[] yVal = new double[translated_sites.Length];
        for (int i = 0; i < translated_sites.Length; i++) {
            xVal[i] = translated_sites[i].coord.x;
            yVal[i] = translated_sites[i].coord.y;
        }
        Voronoi voronoi = new Voronoi(0.1f);
        List<GraphEdge> to_translate =  voronoi.generateVoronoi(translated_sites, xVal, yVal, 0, mapSize - 1, 0, mapSize - 1);
        extract_data_from_graph_edge_list(to_translate, ref metaData, mapSize);
        validate_all_buffers(ref metaData);
    }

    /// <summary>
    /// interfaces the fortune line voronoi and the rest of the programm
    /// </summary>
    /// <param name="cellPos"></param>
    /// <param name="mapSize"></param>
    /// <returns></returns>
    public static VoronoiData getVoronoi(List<Vector2> cellPos, int mapSize) {
        VoronoiData ret = new VoronoiData();
        ret.allCores = new List<CellCore>();
        ret.allNodes = new List<Node>();
        ret.allEdges = new List<Edge>();
        foreach(Vector2 position in cellPos) {
            CellCore buffer = new CellCore(position);
            ret.allCores.Add(buffer);
        }
        getVoronoi(ref ret, mapSize);
        return ret;
    }

    /// <summary>
    /// interfaces with getVoronoi(PreMetaData, int)
    /// </summary>
    /// <param name="xVal"></param>
    /// <param name="yVal"></param>
    /// <param name="mapSize"></param>
    /// <returns></returns>
    public static VoronoiData getVoronoi(float[] xVal, float[] yVal, int mapSize) {
        VoronoiData ret = new VoronoiData();
        ret.allCores = new List<CellCore>();
        ret.allNodes = new List<Node>();
        ret.allEdges = new List<Edge>();
        if (xVal.Length != yVal.Length) {
            throw new Exception("Yhe x and y arrays cannot have different lengths");
        }
        int i = 0;
        while (i < xVal.Length) {
            ret.allCores.Add(new CellCore(new Vector2(xVal[i], yVal[i])));
            i += 1;
        }
        getVoronoi(ref ret, mapSize);
        return ret;
    }

    /// <summary>
    /// casts all of the doubles into floats in order to call the getVoronoi(float[], float[], int)
    /// </summary>
    /// <param name="xVal"></param>
    /// <param name="yVal"></param>
    /// <param name="mapSize"></param>
    /// <returns></returns>
    public static VoronoiData getVoronoi(double[] xVal, double[] yVal, int mapSize) {
        float[] xValues = new float[xVal.Length];
        for(int i = 0; i < xVal.Length; i++) {
            xValues[i] = (float)xVal[i];
        }
        float[] yValues = new float[yVal.Length];
        for (int i = 0; i < yVal.Length; i++) {
            yValues[i] = (float)yVal[i];
        }
        return getVoronoi(xValues, yValues, mapSize);
    }
}
