# FortuneLine_voronoi
This is an implementation of a C# fortune line of witch you can find the source code <a href="https://www.codeproject.com/Tips/797123/Fast-Voronoi-Diagram-in-Csharp">here</a>

This code is intended to work with Unity3d projects but will also work on C# projects

## Voronoi data structure
When using this program, a `VoronoiData` class is returned such as follows :

    public class VoronoiData {
        public List<CellCore> cores;
        public List<Node> nodes;
        public List<Edge> edges;
    }

The `CellCore` class contains all of the data on the cell, it also references it's edges, vertices and neighbour cells<br/>
The `Node` class represents the vertices of the diagram, each one references it's cellcore parents, neighbour vertices and related edges<br/>
The `Edge` class references both related cells and vertices<br/>

## API

The file getVoronoï.cs contains all of the API methods needed to get your voronoï diagram, the prototypes are as follow :

    public static VoronoiData getVoronoi(double[] xVal, double[] yVal, int mapSize);
    public static VoronoiData getVoronoi(float[] xVal, float[] yVal, int mapSize);
    public static VoronoiData getVoronoi(List<CellCore> cells, int mapSize);
    public static VoronoiData getVoronoi(List<Vector2> cellPos, int mapSize);

The programm needs the coordinates of the cellcores in order to be able to work, they can be given as doubles, floats, Vector2 or directly as CellCore.<br/>
The coordinates of the cellcores should always range from 0 to `mapSize` for a correct generation<br/>

## Note on the architechture of this code

This code is an encapsulation of an already working fortune line made by <a href="https://www.codeproject.com/script/Membership/View.aspx?mid=10947831">Burhan Joukhadar</a><br/>
This project encapsulates his code witch only returns a graph made of edges and adds the classes for the vertices and the cellcores in order to have the relations of these different elements.

## Possible uses of this programm

This project was first intended to be used for procedural generation ( hence all of the different relations between the classes ) but could be used wherever a Voronoï diagram is needed.<br />
I will however add that should you only need the edge graph, you are better of using the <a href="https://www.codeproject.com/Tips/797123/Fast-Voronoi-Diagram-in-Csharp">original code</a> as you will not have all of the extra features slowing down your programm.


