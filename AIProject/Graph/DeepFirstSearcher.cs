using StateRepresentation;
using System;
using System.Threading;

namespace AIProject.Graph
{

    public class DeepFirstSearcher : GraphSearcher
    {
        private void Init(State start)
        {
            opens.Add(new Node(start));
        }

        public DeepFirstSearcher(State start)
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
                        opens.Insert(0, newNode);
                }
        }

        public override void Search()
        {
            Node currentNode = null;
            while (opens.Count > 0)
            {
                if (MainWindow.IS_PRINT_DETAILED_SOLUTION)
                {
                    PrintDatabase();
                }
                currentNode = opens[0];
                if (currentNode.CurrentState.IsGoalState())
                {
                    goalStates.Add(currentNode);
                    if (MainWindow.IS_PRINT_DETAILED_SOLUTION || MainWindow.IS_PRINT_SOLUTION)
                    {
                        Console.WriteLine("solution:");
                        PrintSolution(currentNode);
                    }
                    break;
                }
                opens.Remove(currentNode);
                closeds.Add(currentNode);
                Extract(currentNode);
                lock (MainWindow.opens)
                {
                    MainWindow.opens.Clear();
                    foreach (Node n in opens)
                        MainWindow.opens.Add(n);

                }
                lock (MainWindow.closeds)
                {
                    MainWindow.closeds.Clear();
                    foreach (Node n in closeds)
                        MainWindow.closeds.Add(n);

                }
                Thread.Sleep(MainWindow.TIME_BETWEEN_ITERATIONS);
            }
            Console.WriteLine("DFS opens: " + opens.Count +
                               ", closeds: " + closeds.Count);
            lock (MainWindow.SearchIsReady)
            {
                MainWindow.SearchIsReady = currentNode;
            }
        }

        public override string ToString()
        {
            return "Deep first search.\n";
        }
    }
}
