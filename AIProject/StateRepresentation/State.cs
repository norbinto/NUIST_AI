using System;
using System.Collections.Generic;

namespace StateRepresentation
{
    public class OperatorDoesNotExistsException : Exception
    {
        public override string ToString()
        {
            return "Operator does not exists!";
        }
    }

    public abstract class State
    {

        protected static ICollection<Operator> operators;
        public static ICollection<Operator> Operators
        {
            get
            {
                return operators;
            }
        }

        public abstract bool IsGoalState();
        public abstract bool IsApplicatable(Operator op);
    }
}
