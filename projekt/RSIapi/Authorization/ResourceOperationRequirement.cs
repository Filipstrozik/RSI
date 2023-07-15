using Microsoft.AspNetCore.Authorization;

namespace RSIapi.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }

    public class ResourceOperationRequirement : IAuthorizationRequirement
    {

        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            Operation = resourceOperation;
        }

        public ResourceOperation Operation { get; private set;}
    }
}
