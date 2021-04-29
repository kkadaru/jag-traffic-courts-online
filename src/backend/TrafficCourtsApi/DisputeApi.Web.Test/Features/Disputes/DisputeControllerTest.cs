using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using DisputeApi.Web.Features;
using DisputeApi.Web.Features.Disputes;
using DisputeApi.Web.Features.Disputes.Commands;
using DisputeApi.Web.Features.Disputes.Queries;
using DisputeApi.Web.Test.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DisputeApi.Web.Test.Features.Disputes
{
    [ExcludeFromCodeCoverage]
    public class DisputeControllerTest
    {
        private Mock<ILogger<DisputesController>> _loggerMock;
        private Mock<IMediator> _mediatorMock;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = LoggerServiceMock.LoggerMock<DisputesController>();
            _mediatorMock = new Mock<IMediator>();
        }

        [Test]
        public void throw_ArgumentNullException_if_passed_null()
        {
            Assert.Throws<ArgumentNullException>(() => new DisputesController(null, _mediatorMock.Object));
            Assert.Throws<ArgumentNullException>(() => new DisputesController(_loggerMock.Object, null));
        }

        [Theory]
        [AutoData]
        public async Task get_disputes(GetDisputeResponse dispute)
        {
            IEnumerable<GetDisputeResponse> expected = new List<GetDisputeResponse> {dispute};

            _mediatorMock.Setup(x => x.Send(It.IsAny<GetAllDisputesQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expected));

            var sut = new DisputesController(_loggerMock.Object, _mediatorMock.Object);

            var result = await sut.GetDisputes();
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            var objectResult = (ObjectResult) result;
            Assert.IsInstanceOf<ApiResultResponse<IEnumerable<GetDisputeResponse>>>(objectResult.Value);

            var values = ((ApiResultResponse<IEnumerable<GetDisputeResponse>>) objectResult.Value).Result;
            Assert.AreEqual(values.Count(), 1);

            _mediatorMock.Verify(x => x.Send(It.IsAny<GetAllDisputesQuery>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task get_dispute(GetDisputeResponse expected)
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetDisputeQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expected));

            var sut = new DisputesController(_loggerMock.Object, _mediatorMock.Object);

            var result = await sut.GetDispute(expected.Id);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);

            Assert.IsInstanceOf<ApiResultResponse<GetDisputeResponse>>(((OkObjectResult) result).Value);
            _mediatorMock.Verify(x => x.Send(It.IsAny<GetDisputeQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task returns_not_found_if_dispute_service_returns_null(int disputeId)
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetDisputeQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<GetDisputeResponse>(null));

            var sut = new DisputesController(_loggerMock.Object, _mediatorMock.Object);

            var result = await sut.GetDispute(disputeId);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Theory]
        [AutoData]
        public async Task create_dispute(CreateDisputeCommand dispute, CreateDisputeResponse expected)
        {
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateDisputeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<CreateDisputeResponse>(expected));

            var sut = new DisputesController(_loggerMock.Object, _mediatorMock.Object);

            var result = await sut.CreateTicketDispute(dispute);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateDisputeCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Theory]
        [AutoData]
        public async Task when_mediator_return_id_is_0_createDispute_return_badRequest(CreateDisputeCommand dispute,
            CreateDisputeResponse response)
        {
            response.Id = 0;
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateDisputeCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<CreateDisputeResponse>(response));

            var sut = new DisputesController(_loggerMock.Object, _mediatorMock.Object);

            var result = (BadRequestObjectResult) await sut.CreateTicketDispute(dispute);
            Assert.IsNotNull(result);
            _mediatorMock.Verify(x => x.Send(It.IsAny<CreateDisputeCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}