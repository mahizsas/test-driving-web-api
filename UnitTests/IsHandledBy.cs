using NUnit.Framework.Constraints;

namespace UnitTests
{
    public class IsHandledBy
    {
        public static IResolveConstraint Controller(string controller)
        {
            return new ControllerEqualityConstraint(controller);
        }

        public static IResolveConstraint Action(string action)
        {
            return new ActionEqualityConstraint(action);
        }
    }
}