using StateRepresentation;
using System;

namespace AIProject.Graph
{
    class BreadthFirstSearcher : GraphSearcher
    {
        private void Init(State start)
        {
            opens.Add(new Node(start));
        }

        public BreadthFirstSearcher(State start)
        {
            Init(start);
        }

        protected override void Extract(Node node)
        {
            foreach (Operator op in State.Operators)
                if (node.CurrentState.IsApplicatable(op))
                {
                    Node newNode = new Node(node, op);
                    if (!(opens.Contains(newNode) || closeds.Contains(newNode)))
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
                Node currentNode = opens[0];
                if (currentNode.CurrentState.IsGoalState())
                {
                    goalStates.Add(currentNode);
                    if (MainWindow.ISPRINTDETAILEDSOLUTION || MainWindow.ISPRINTSOLUTION)
                    {
                        Console.WriteLine("solution:");
                        PrintSolution(currentNode);
                    }
                    break;
                }
                opens.Remove(currentNode);
                closeds.Add(currentNode);
                Extract(currentNode);
            }
            Console.WriteLine("BFS opens: " + opens.Count +
                               ", closeds: " + closeds.Count);
        }

        public override string ToString()
        {
            return "Breadth first search.\n";
        }
    }
}
