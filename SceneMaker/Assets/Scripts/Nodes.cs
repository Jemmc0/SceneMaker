using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[ExecuteInEditMode]
public class Nodes : MonoBehaviour
{
    public Pathfinding control;

    public LayerMask nodeLayer;
    public LayerMask obstacleLayer;
    public Dictionary<Nodes, float> neighbours = new Dictionary<Nodes, float>();
    public Nodes previous;
    public float G { get; set; } //G es el costo más bajo actualmente en llegar desde el nodo inicial a este nodo.
    public float H { get; set; } //H es la heuristica, es un estimativo del menor costo posible para llegar de este nodo 
                                 //al nodo objetivo. LA HEURISTICA NO PUEDE SOBREESTIMAR.
    public float _g;
    public float _h;
    public float _f;

    public float F { get { return G + H; } } //F es la prioridad de los nodos a la hora de ser visitados. El nodo con menor F
                                             //va a ser el nodo a visitar.
    public float radius;
    public bool walkable;
    public float currentDistance = Mathf.Infinity;

    private bool render;


    void Awake()
    {
        control = GameObject.Find("Controller").GetComponent<Pathfinding>();
        //nodeLayer = control.nodeLayer;
        //obstacleLayer = control.obstaclesLayer;
        radius = control._radius;

        // neighbours.Clear();
        Bake();

    }
    public void Bake()
    {
        neighbours.Clear();
        previous = null;
        G = Mathf.Infinity;

        //Consiguo los nodos cercanos a mi nodo.
        var nearNodes = Physics.OverlapSphere(transform.position, radius, nodeLayer);

        foreach (var nodeCollider in nearNodes)
        {
            var node = nodeCollider.GetComponent<Nodes>();
            //Nos aseguramos de que sea un nodo y que no sea yo mismo. 
            if (node != null &&
                node != this)
            {
                //Conseguimos la distancia hacia nuestro vecino
                var dist = Vector3.Distance(transform.position, node.transform.position);
                //Lo agregamos al diccionario.
                neighbours.Add(node, dist);
            }
        }
    }



    private void Update()
    {
        // Bake();
        radius = control._radius;
        render = control.render;
        _g = H;
        _h = G;
        _f = F;
        /*  if (control.reCast)
          {
      foreach (var item in neighbours)
          {
              item.
          }
              Bake();

              control.reCast = false;
          }*/

    }

    public void Reset()
    {

        G = Mathf.Infinity;
        previous = null;
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.black;

        if (control.render)
        {


            Gizmos.color = Color.black;

            foreach (var neighbour in neighbours)
            {
                Gizmos.DrawLine(transform.position, neighbour.Key.transform.position);
            }

            Gizmos.color = !walkable ? Color.red : Color.blue;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }

    }



    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

    }


            }








