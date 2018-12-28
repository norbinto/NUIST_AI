using StateRepresentation;
using System;

namespace AIProject.MazeGame
{
    public class OperatorLeft : Operator, CostOperator
    {
        public double Cost(State state)
        {
            return 1;
        }

        public override State Invoke(State currentState)
        {
            if (currentState != null && currentState is MazeState)
            {
                var ret = new MazeState();
                ret.PosY = ((MazeState)currentState).PosY;
                ret.PosX = ((MazeState)currentState).PosX - 1;
                return ret;
            }
            throw new Exception("Operator type exception");
        }

    }
}
