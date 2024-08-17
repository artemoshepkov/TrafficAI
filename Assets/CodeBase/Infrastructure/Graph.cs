using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public static class FloatExtension
    {
        private static float _eps = 0.003f;
        public static bool EqualTo(this float dot, float f) => dot >= f - _eps && dot <= f + _eps;
    }
    public class Graph
    {
        public static readonly List<Node> Nodes = new List<Node>();
        public static CellComponent[,] Grid { get; set; }

        public static void OnRoadChanged(int i, int j)
        {
            var nodes = Grid[i, j];

            
            if (i > 0)
            {
                var upNodes = Grid[i - 1, j];

                if (upNodes != null)
                {
                    nodes.ConnectCell(upNodes, Dir.Up);
                    upNodes.ConnectCell(nodes, Dir.Down);

                    // upNodes.GetNodesToConnect(Dir.Up)?.ConnectNode(nodes.GetNodesToConnect(Dir.Up));
                    // nodes.GetNodesToConnect(Dir.Down)?.ConnectNode(upNodes.GetNodesToConnect(Dir.Down));
                }
            }
            if (i < Grid.Length - 1)
            {
                var downNodes = Grid[i + 1, j];

                if (downNodes != null)
                {
                    nodes.ConnectCell(downNodes, Dir.Down);
                    downNodes.ConnectCell(nodes, Dir.Up);

                    // downNodes.GetNodesToConnect(Dir.Down)?.ConnectNode(nodes.GetNodesToConnect(Dir.Down));
                    // nodes.GetNodesToConnect(Dir.Up)?.ConnectNode(downNodes.GetNodesToConnect(Dir.Up));
                }
            }
            if (j > 0)
            {
                var leftNodes = Grid[i, j - 1];

                if (leftNodes != null)
                {
                    nodes.ConnectCell(leftNodes, Dir.Left);
                    leftNodes.ConnectCell(nodes, Dir.Right);

                    // leftNodes.GetNodesToConnect(Dir.Left)?.ConnectNode(nodes.GetNodesToConnect(Dir.Left));
                    // nodes.GetNodesToConnect(Dir.Right)?.ConnectNode(leftNodes.GetNodesToConnect(Dir.Right));
                }
            }
            if (j < Grid.GetLength(1) - 1)
            {
                var rightNodes = Grid[i, j + 1];

                if (rightNodes != null)
                {
                    nodes.ConnectCell(rightNodes, Dir.Right);
                    rightNodes.ConnectCell(nodes, Dir.Left);

                    // rightNodes.GetNodesToConnect(Dir.Right)?.ConnectNode(nodes.GetNodesToConnect(Dir.Right));
                    // nodes.GetNodesToConnect(Dir.Left)?.ConnectNode(rightNodes.GetNodesToConnect(Dir.Left));
                }   
            }
        }
        
        public static void ConnectRoads(CellComponent fromCell, CellComponent toCell)
        {
            // foreach (Node fromNode in fromCell.Nodes)
            // {
            //     foreach (Node toNode in toCell.Nodes)
            //     {
            //         var dotX = Vector3.Dot(fromNode.Forward, toNode.Forward);
            //
            //         if (FloatExtension.EqualTo(dotX, 1f))
            //         {                    
            //             if (!fromNode.ConnectedNodes.Contains(toNode))
            //                 fromNode.ConnectNode(toNode);
            //             else
            //                 toNode.ConnectNode(fromNode);
            //         }
            //     }
            // }
        }

        private static float GetFloat(float f)
        {
            if (FloatExtension.EqualTo(f, 0f))
                return 0f;
            return f;
        }

        public static Dir GetDirection(Vector3 dir)
        {
            if (FloatExtension.EqualTo(dir.x, 1f))
                return Dir.Left;
            if (FloatExtension.EqualTo(dir.x, -1f))
                return Dir.Right;
            if (FloatExtension.EqualTo(dir.z, 1f))
                return Dir.Down;
            if (FloatExtension.EqualTo(dir.z, -1f))
                return Dir.Up;
            return Dir.None;
        }
    }
}