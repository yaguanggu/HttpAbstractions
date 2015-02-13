// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Microsoft.AspNet.Http.Interfaces.Security
{
    public interface ISignOutContext 
    {
        IEnumerable<string> AuthenticationSchemes { get; }

        void Accept(string authenticationScheme, IDictionary<string, object> description);
    }
}