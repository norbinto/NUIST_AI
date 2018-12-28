using System;
using System.Collections.Generic;

namespace AIProject.Graph
{
    public abstract class Searcher
    {
        protected IList<Node> goalStates;
        public IList<Node> GoalStates
        {
            get
            {
                return goalStates;
            }
        }

        protected Searcher()
        {
            goalStates = new List<Node>();
        }



        public void PrintSolution(Node currentNode)
        {
            if (currentNode != null)
            {
                PrintSolution(currentNode.Parent);
                Console.WriteLine(currentNode);
            }
        }

        public abstract void Search();
    }
}
