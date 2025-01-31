﻿// Copyright (c) Microsoft. All rights reserved.

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SemanticKernel.Experimental.Assistants.Extensions;
using Microsoft.SemanticKernel.Experimental.Assistants.Internal;
using Moq;
using Moq.Protected;
using Xunit;

namespace SemanticKernel.Experimental.Assistants.UnitTests.Extensions;

[Trait("Category", "Unit Tests")]
[Trait("Feature", "Assistant")]
public sealed class OpenAIRestExtensionsThreadTests
{
    private const string BogusApiKey = "bogus";
    private const string TestThreadId = "threadId";

    private readonly OpenAIRestContext _restContext;
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler = new();

    public OpenAIRestExtensionsThreadTests()
    {
        this._mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{}") });
        this._restContext = new(BogusApiKey, () => new HttpClient(this._mockHttpMessageHandler.Object));
    }

    [Fact]
    public async Task CreateThreadModelAsync()
    {
        await this._restContext.CreateThreadModelAsync().ConfigureAwait(true);

        this._mockHttpMessageHandler.VerifyMock(HttpMethod.Post, 1, OpenAIRestExtensions.BaseThreadUrl);
    }

    [Fact]
    public async Task GetThreadModelAsync()
    {
        await this._restContext.GetThreadModelAsync(TestThreadId).ConfigureAwait(true);

        this._mockHttpMessageHandler.VerifyMock(HttpMethod.Get, 1, OpenAIRestExtensions.GetThreadUrl(TestThreadId));
    }

    [Fact]
    public async Task DeleteThreadModelAsync()
    {
        await this._restContext.DeleteThreadModelAsync(TestThreadId).ConfigureAwait(true);

        this._mockHttpMessageHandler.VerifyMock(HttpMethod.Delete, 1, OpenAIRestExtensions.GetThreadUrl(TestThreadId));
    }
}
