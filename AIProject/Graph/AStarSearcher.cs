using AIProject.MazeGame;
using StateRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject.Graph
{
    public class AStarSearcher : GraphSearcher
    {
        private void Init(State start)
        {
            opens.Add(new AStarNode(start));
        }

        public AStarSearcher(State start)
        {
            Init(start);
        }

        protected override void Extract(Node node)
        {
            foreach (Operator op in State.Operators)
                if (node.CurrentState.IsApplicatable(op))
                {
                    AStarNode newNode = new AStarNode((AStarNode)node, op);
                    int index;
                    if ((index = opens.IndexOf(newNode)) != -1)
                    {
                        AStarNode oldNode = (AStarNode)opens[index];
                        if (newNode.Cost < oldNode.Cost)
                        {
                            opens.Remove(oldNode);
                            opens.Add(newNode);
                        }
                    }
                    else if ((index = closeds.IndexOf(newNode)) != -1)
                    {
                        AStarNode oldNode = (AStarNode)closeds[index];
                        if (newNode.Cost < oldNode.Cost)
                        {
                            closeds.Remove(oldNode);
                            opens.Add(newNode);
                        }
                    }
                    else
                        opens.Add(newNode);
                }
        }

        public override void Search()
        {
            while (opens.Count > 0)
            {
                if (MainWindow.ISPRINTDETAILEDSOLUTION)
                {
                    PrintDatabase();
                }
                AStarNode currentNode = (AStarNode)opens[0];
                if (currentNode.CurrentState.IsGoalState())
                {
                    goalStates.Add(currentNode);
                    if (MainWindow.ISPRINTDETAILEDSOLUTION || MainWindow.ISPRINTSOLUTION)
                    {
                        Console.WriteLine("solution");
                        PrintSolution(currentNode);
                    }
                    break;
                }
                opens.Remove(currentNode);
                closeds.Add(currentNode);
                Extract(currentNode);
                opens.Sort(new AAlgoritmusSorter());
            }
            Console.WriteLine("A Star opens: " + opens.Count +
                               ", closeds: " + closeds.Count);
        }

        public override string ToString()
        {
            return " A star search.\n";
        }



        private class AAlgoritmusSorter : IComparer<Node>
        {
            int IComparer<Node>.Compare(Node node1, Node node2)
            {
                AStarNode aNode1 = (AStarNode)node1,
                                 aNode2 = (AStarNode)node2;
                return ((aNode1.CurrentState as HeuristicState).Heuristic() + aNode1.Cost).CompareTo(
                         (aNode2.CurrentState as HeuristicState).Heuristic() + aNode2.Cost);
            }
        }



    }
}
