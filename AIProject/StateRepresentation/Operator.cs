namespace StateRepresentation
{
  public abstract class Operator
  {
       public abstract State Invoke(State currentState);
  }
}
