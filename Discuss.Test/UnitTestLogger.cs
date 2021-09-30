using Discuss.SignalR.Hubs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using NLog.Web;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discuss.SignalR.Models;

namespace Discuss.Test
{
    [TestFixture]
    public class UnitTestLogger
    {
        Mock<ILogger<CommunicationHub>> _loggerMock;
        CommunicationHub _hub;

        [OneTimeSetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CommunicationHub>>();
            _hub = new CommunicationHub(_loggerMock.Object);
        }

        [TestCase("1 Test run")]
        public async Task LogErrorMessage(string info)
        {
            var msg = new MessageData() { Date = DateTime.Now, Message = "Test", DestinationClientId="cl1", SendingClientId="cl2", MessageId="000-000-000" };
            await _hub.SendMessageToClient(msg);

            _loggerMock.Verify(
                m => m.Log(LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Error occured")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once
            );
        }
    }
}
