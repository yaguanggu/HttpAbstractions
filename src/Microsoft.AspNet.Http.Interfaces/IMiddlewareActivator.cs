using System;
using Microsoft.Framework.Runtime;

namespace Microsoft.AspNet.Http.Interfaces
{
    [AssemblyNeutral]
    public interface IMiddlewareActivator
    {
        object CreateInstance(Type middlewareType, object[] parameters);
    }
}