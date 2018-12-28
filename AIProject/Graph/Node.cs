using StateRepresentation;

namespace AIProject.Graph
{
    public class Node
    {
        protected State currentState;
        public State CurrentState
        {
            get
            {
                return currentState;
            }
        }

        protected Node parent;
        public Node Parent
        {
            get
            {
                return parent;
            }
        }

        protected Operator oper;
        public Operator Oper
        {
            get
            {
                return oper;
            }
        }

        protected int depth;
        public int Depth
        {
            get
            {
                return depth;
            }
        }

        public Node(State start)
        {
            currentState = start;
            parent = null;
            oper = null;
            depth = 0;
        }

        public Node(Node parent, Operator oper)
        {
            currentState = oper.Invoke(parent.currentState);
            this.parent = parent;
            this.oper = oper;
            depth = parent.depth + 1;
        }

        public override bool Equals(object obj)
        {
            return obj is Node && currentState.Equals(((Node)obj).currentState);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return (oper == null ? "" : oper + " => ") +currentState.ToString();
        }
    }
}
