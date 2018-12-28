using AIProject.MazeGame;
using StateRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject.Graph
{
    public class AStarNode : Node
    {
        protected double cost;
        public double Cost
        {
            get
            {
                return cost;
            }
        }

        public AStarNode(State start)
          : base(start)
        {
            cost = 0;
        }

        public AStarNode(AStarNode parent, Operator oper)
          : base(parent, oper)
        {
            cost = parent.cost + (oper is CostOperator ?
              ((CostOperator)oper).Cost(parent.CurrentState) : 1);
        }

        public override string ToString()
        {
            return base.ToString() + ", cost=" + cost;
        }
    }
}
