using StateRepresentation;
using System;
using System.Collections.Generic;

namespace AIProject.MazeGame
{
    public class MazeState : HeuristicState
    {
        private int _posX;
        private int _posY;

        public int PosX { get => _posX; set => _posX = value; }
        public int PosY { get => _posY; set => _posY = value; }

        static MazeState()
        {
            operators = new List<Operator>
            {
                new OperatorDown(),
                new OperatorUp(),
                new OperatorLeft(),
                new OperatorRight()
            };
        }

        public MazeState()
        {
            PosX = Map.GetStartPositonX();
            PosY = Map.GetStartPositonY();
        }

        public override bool IsApplicatable(Operator op)
        {
            var tmpState = op.Invoke(this);
            if (tmpState is MazeState && Map.CurrentMap[((MazeState)tmpState).PosY, ((MazeState)tmpState).PosX] > 0)
            {
                return true;
            }
            return false;
        }

        public override bool IsGoalState()
        {
            return Map.CurrentMap[PosY, PosX] == 3;
        }

        public override string ToString()
        {
            return "X:" + PosX + " Y:" + PosY;
        }

        public override bool Equals(object obj)
        {
            if (obj is MazeState && PosX == ((MazeState)obj).PosX && PosY == ((MazeState)obj).PosY)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected double heuristic;
        public override double Heuristic()
        {
            if (Math.Abs(heuristic) < double.Epsilon)
                InitHeuristic();
            return heuristic;
        }

        private void InitHeuristic()
        {
            heuristic = ComputeHeuristic(PosX, PosY, Map.GetGoalPositionX(), Map.GetGoalPositionY());
        }

        private double ComputeHeuristic(int posX, int posY, int goalX, int goalY)
        {
            return Math.Pow(posX - goalX, 2) + Math.Pow(posY - goalY, 2);
        }

    }
}
