// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Security.Claims;

namespace Microsoft.AspNet.Http.Interfaces.Security
{
    public interface IAuthenticateContext
    {
        IEnumerable<string> AuthenticationSchemes { get; }

        void Authenticated(ClaimsIdentity identity, IDictionary<string, string> properties, IDictionary<string, object> description);

        void NotAuthenticated(string authenticationScheme, IDictionary<string, string> properties, IDictionary<string, object> description);
    }
}
