using System;
using System.Collections.Generic;

namespace AIProject.Graph
{
    public abstract class GraphSearcher : Searcher
    {
        protected List<Node> opens, closeds;

        private void Init()
        {
            opens = new List<Node>();
            closeds = new List<Node>();
        }

        protected GraphSearcher()
        {
            Init();
        }


        protected void PrintDatabase()
        {
            Console.WriteLine("Open nodes:");
            foreach (Node node in opens)
                Console.WriteLine(node);
            Console.WriteLine("Closed nodes:");
            foreach (Node node in closeds)
                Console.WriteLine(node);
            Console.WriteLine();
        }
                
        protected abstract void Extract(Node node);
    }
}
