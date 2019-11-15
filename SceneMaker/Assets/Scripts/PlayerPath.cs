using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPath : MonoBehaviour
{
    public Nodes initialNode;
    public Nodes endNode;

    public Nodes _initialNode;
    public Nodes _endNode;

    public GameObject end;

    public List<Nodes> AstarPath = new List<Nodes>();
    public List<Nodes> DikjstraPath = new List<Nodes>();
    private List<Nodes> closedNodes = new List<Nodes>();
    private Queue<Nodes> openNodes = new Queue<Nodes>();
    private Stack<Nodes> pathNodes = new Stack<Nodes>();

    private List<Nodes> showNodes = new List<Nodes>();




    private List<Nodes> _closedNodes = new List<Nodes>();
    private List<Nodes> _openNodes = new List<Nodes>();
    private Stack<Nodes> _pathNodes = new Stack<Nodes>();

    //Lista que usamos unicamente para mostrar los nodos que agrego a openNodes.
    private List<Nodes> _showNodes = new List<Nodes>();

    private Camera Cam;
    Pathfinding control;

    public void Start()
    {
        control = GameObject.Find("Controller").GetComponent<Pathfinding>();
        Cam = GameObject.Find("Main Camera").GetComponent<Camera>();

            _pathNodes = AStar(_initialNode, _endNode);
        
 
            pathNodes = ExecuteDijkstra(initialNode, endNode);
        
    }

    private void Update()
    {

        Vector3 temp = Input.mousePosition;
        temp.z = 12; // Set this to be the distance you want the object to be placed in front of the camera.  
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(temp);
        if (Input.GetKeyDown(KeyCode.S))
        {

            if (control.astar)
            {
                _pathNodes.Clear(); 
                DikjstraPath.Clear();
                AstarPath.Clear();
                _pathNodes = AStar(_initialNode, _endNode);
     
            }
            else if (control.dijkstra)
            {
               // Nodes node = Instantiate(end.GetComponent<Nodes>(), mousepos, gameObject.transform.rotation);
                //endNode = node;
               // endNode.Reset();
                pathNodes.Clear();
                AstarPath.Clear();
                DikjstraPath.Clear();
                pathNodes = ExecuteDijkstra(initialNode, endNode);
            }

        }
        foreach (var item in _pathNodes)
        {
            if (control.astar)
            {
                if (AstarPath.Count != _pathNodes.Count)
                {
                    AstarPath.Add(item);
                }
            }
            else
            {
                AstarPath.Clear();
            }

        }
        foreach (var item in pathNodes)
        {
            if (control.dijkstra)
            {
                if (DikjstraPath.Count != pathNodes.Count)
                {
                    DikjstraPath.Add(item);
                }
            }
            else
            {
                DikjstraPath.Clear();
            }
           
        }

    }

    public Stack<Nodes> ExecuteDijkstra(Nodes initial, Nodes end)
    {
        //Al reutilizar la función, reseteamos los valores de los nodos.
        foreach (var item in closedNodes)
        {
            item.currentDistance = Mathf.Infinity;
        }
        foreach (var item in openNodes)
        {
            item.currentDistance = Mathf.Infinity;
        }

        Stack<Nodes> resultPath = new Stack<Nodes>();
        //Limpiamos las listas.
        closedNodes.Clear();
        openNodes.Clear();

        //Agregamos nuestro nodo inicial al Queue de los nodos a visitar
        openNodes.Enqueue(initialNode);
        //Reiniciamos los valores del nodo.
        initialNode.currentDistance = 0;
        initialNode.previous = null;

        while (openNodes.Count > 0)
        {
            //Tomamos un elemento del Queue. No es necesario removerlo más adelante porque el Dequeue ya lo remueve a diferencia de utilizar una lista.
            Nodes current = openNodes.Dequeue();

            //Si el nodo actual es el destino.
            if (current == endNode)
            {
                //Uso un stack para el camino final ya que como estamos empezando desde el nodo final hasta el nodo inicial vamos a tener el camino al revés, pero
                //si me lo guardo en un stack ya lo invierto.
                while (current != null)
                {
                    resultPath.Push(current);
                    current = current.previous;
                }

                return resultPath;
            }

            foreach (var item in current.neighbours)
            {
                var neighNode = item.Key;
                var neighDist = item.Value;

                //Si el nodo vecino ya fue analizado o está bloqueado continuamos a la siguiente iteración.
                if (closedNodes.Contains(neighNode) || !neighNode.walkable)
                {
                    continue;
                }

                //Evitamos repetir nodos preguntando primero si están en el Queue antes de agregarlo.
                if (!openNodes.Contains(neighNode))
                {
                    openNodes.Enqueue(neighNode);
                }

                //Preguntamos si el camino que estamos analizando es más barato que el que ya tiene el nodo.
                if (current.currentDistance + neighDist < neighNode.currentDistance)
                {
                    //Reemplazo los valores en caso de que hayamos encontrado una mejor opción.
                    neighNode.currentDistance = current.currentDistance + neighDist;
                    neighNode.previous = current;
                }

                


            }

            //Analizado todos sus vecinos, agregamos el nodo actual a la lista de nodos ya revisados para evitar loops infinitos.
            closedNodes.Add(current);
        }

        //Si llegué hasta acá es porque no había forma de llegar al nodo final desde el nodo inicial.
        return null;
    }



    public Stack<Nodes> AStar(Nodes initial, Nodes end)
    {
        //Al reutilizar la función, reseteamos los valores de los nodos.
        foreach (var item in _closedNodes)
        {
            item.Reset();
        }
        foreach (var item in _openNodes)
        {
            item.Reset();
        }

        Stack<Nodes> resultPath = new Stack<Nodes>();
        //Limpiamos las listas.
        _closedNodes.Clear();
        _openNodes.Clear();
        _showNodes.Clear();

        //Agregamos nuestro nodo inicial a la lista de los nodos a visitar
        _openNodes.Add(_initialNode);
        _showNodes.Add(_initialNode);
        //Reiniciamos los valores del nodo.
        _initialNode.G = 0;
        _initialNode.previous = null;

        while (_openNodes.Count > 0)
        {
            //Tomamos un elemento de la lista de openNodes. Vamos a conseguir el nodo con menor F.
            Nodes current = LookForLowerF();

            //Si el nodo actual es el destino.
            if (current == _endNode)
            {
                //Uso un stack para el camino final ya que como estamos empezando desde el nodo final hasta el nodo inicial vamos a tener el camino al revés, pero
                //si me lo guardo en un stack ya lo invierto.
                while (current != null)
                {
                    resultPath.Push(current);
                    current = current.previous;
                }

                return resultPath;
            }

            foreach (var item in current.neighbours)
            {
                var neighNode = item.Key;
                var neighDist = item.Value;

                //Si el nodo vecino ya fue analizado o está bloqueado continuamos a la siguiente iteración.
                if (_closedNodes.Contains(neighNode) || !neighNode.walkable)
                {
                    continue;
                }

                //Evitamos repetir nodos preguntando primero si están en el Queue antes de agregarlo.
                if (!_openNodes.Contains(neighNode))
                {
                    //Calculo por unica vez el costo de la H del nodo. En este caso, la forma más corta de llegar al objetivo
                    //es yendo en linea recta.
                    neighNode.H = Vector3.Distance(neighNode.transform.position, end.transform.position);

                    _openNodes.Add(neighNode);
                    _showNodes.Add(neighNode);
                }

                //Preguntamos si el camino que estamos analizando es más barato que el que ya tiene el nodo.
                if (current.G + neighDist < neighNode.G)
                {
                    //Reemplazo los valores en caso de que hayamos encontrado una mejor opción.
                    neighNode.G = current.G + neighDist;
                    neighNode.previous = current;
                }
              
            }

            //Analizado todos sus vecinos, agregamos el nodo actual a la lista de nodos ya revisados para evitar loops infinitos.
            _closedNodes.Add(current);
            //Remuevo el nodo ya que terminamos de trabajar con él.
            _openNodes.Remove(current);
        }

        //Si llegué hasta acá es porque no había forma de llegar al nodo final desde el nodo inicial.
        return null;
    }

    public Nodes LookForLowerF()
    {
        Nodes nextNode = _openNodes[0];

        foreach (var node in _openNodes)
        {
            if (node.F < nextNode.F)
            {
                nextNode = node;
            }
        }
        return nextNode;
    }

    private void OnDrawGizmos()
    {
             
        if (control.astar && control.render)
        {
            Gizmos.color = Color.yellow;

            foreach (var item in _showNodes)
            {
                Gizmos.DrawSphere(item.transform.position, 0.3f);
            }

            Gizmos.color = Color.green;

            foreach (var item in _pathNodes)
            {
                Gizmos.DrawSphere(item.transform.position, 0.3f);

            }
        }
        else if (control.dijkstra && control.render)
        {
            Gizmos.color = Color.yellow;

            foreach (var item in showNodes)
            {
                Gizmos.DrawSphere(item.transform.position, 0.3f);
            }


            Gizmos.color = Color.green;
            foreach (var item in pathNodes)
            {
                Gizmos.DrawSphere(item.transform.position, 0.3f);
              
                    //   path.Add(item);
                
            }
        }
    }


}
