// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Http.Features
{
    public interface IHttpSendFileFeature
    {
        Task SendFileAsync(string path, long offset, long? length, CancellationToken cancellation);
    }
}