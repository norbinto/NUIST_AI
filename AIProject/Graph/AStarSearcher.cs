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
                    AStarNode uj = new AStarNode((AStarNode)node, op);
                    int index;
                    if ((index = opens.IndexOf(uj)) != -1)
                    {
                        AStarNode regi = (AStarNode)opens[index];
                        if (uj.Cost < regi.Cost)
                        {
                            opens.Remove(regi);
                            opens.Add(uj);
                        }
                    }
                    else if ((index = closeds.IndexOf(uj)) != -1)
                    {
                        AStarNode regi = (AStarNode)closeds[index];
                        if (uj.Cost < regi.Cost)
                        {
                            closeds.Remove(regi);
                            opens.Add(uj);
                        }
                    }
                    else
                        opens.Add(uj);
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
                opens.Sort(new AAlgoritmusRendezo());
            }
            Console.WriteLine("A Star opens: " + opens.Count +
                               ", closeds: " + closeds.Count);
        }

        public override string ToString()
        {
            return " A star search.\n";
        }



        private class AAlgoritmusRendezo : IComparer<Node>
        {
            int IComparer<Node>.Compare(Node node1, Node node2)
            {
                AStarNode aNode1 = (AStarNode)node1,
                                 aNode2 = (AStarNode)node2;
                return ((aNode1.CurrentState as HeuristicState).Heuristic() + aNode1.Cost).CompareTo(
                         (aNode2.CurrentState as HeuristicState).Heuristic() + aNode2.Cost);
#warning 0 heuristics
                //return (0 + aNode1.Cost).CompareTo(
                //         0 + aNode2.Cost);

            }
        }



    }
}
