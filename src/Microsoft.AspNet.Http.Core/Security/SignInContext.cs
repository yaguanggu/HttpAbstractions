// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Http.Interfaces.Security;

namespace Microsoft.AspNet.Http.Core.Security
{
    public class SignInContext : ISignInContext
    {
        private List<string> _accepted;

        public SignInContext([NotNull] ClaimsPrincipal principal, IDictionary<string, string> dictionary)
        {
            Principal = principal;
            Properties = dictionary ?? new Dictionary<string, string>(StringComparer.Ordinal);
            _accepted = new List<string>();
        }

        //public IEnumerable<ClaimsPrincipal> Principals { get; private set; }
        public ClaimsPrincipal Principal { get; private set; }

        public IDictionary<string, string> Properties { get; private set; }

        public IEnumerable<string> Accepted
        {
            get { return _accepted; }
        }

        public void Accept(string authenticationScheme, IDictionary<string, object> description)
        {
            _accepted.Add(authenticationScheme);
        }
    }
}