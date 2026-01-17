using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Plais.Exceptions;
using Plais.Middlewares;
using Plais.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Plais.Middlewares.Tests
{
    public class ErrorHandlingMiddlewareTests
    {
        private readonly Mock<ILogger<ErrorHandlingMiddleware>> _loggerMock;
        private readonly ErrorHandlingMiddleware _middleware;
        private readonly DefaultHttpContext _context;
        private readonly Mock<RequestDelegate> _nextDelegateMock;
        public ErrorHandlingMiddlewareTests()
        {
            _loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            _middleware = new ErrorHandlingMiddleware(_loggerMock.Object);
            _context = new DefaultHttpContext();
            _nextDelegateMock = new Mock<RequestDelegate>();
        }

        [Fact()]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNext()
        {

            await _middleware.InvokeAsync(_context, _nextDelegateMock.Object);

            _nextDelegateMock.Verify(next => next.Invoke(_context), Times.Once);

        }

        [Fact()]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCodeTo404()
        {
            var notFoundException = new NotFoundException(nameof(Bulletin), "1");

            await _middleware.InvokeAsync(_context, _ => throw notFoundException);

            _context.Response.StatusCode.Should().Be(404);
        }

        [Fact()]
        public async Task InvokeAsync_WhenPasswordChangeFailedExceptionThrown_ShouldSetStatusCodeTo400()
        {
            var exception = new PasswordChangeFailedException();

            await _middleware.InvokeAsync(_context, _ => throw exception);

            _context.Response.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task InvokeAsync_WhenInvalidOperationExceptionThrown_ShouldSetStatusCodeTo400()
        {
            var exception = new InvalidOperationException();

            await _middleware.InvokeAsync(_context, _ => throw exception);

            _context.Response.StatusCode.Should().Be(400);
        }

        [Fact()]
        public async Task InvokeAsync_WhenGenericExceptionIsThrown_ShouldSetStatusCodeTo500()
        {
            var exception = new Exception();
            
            await _middleware.InvokeAsync(_context, _ => throw exception);

            _context.Response.StatusCode.Should().Be(500);

        }


    }
}