﻿using CMA.ISMAI.Engine.Automation.Sagas;
using CMA.ISMAI.Engine.Automation.Sagas.ISMAI.Interface;
using CMA.ISMAI.Engine.Automation.Sagas.ISMAI.Service;
using CMA.ISMAI.Logging.Interface;
using CMA.ISMAI.Sagas.Engine.ISMAI.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CMA.ISMAI.UnitTests.Sagas.ISMAI
{
    public class SagaService_Tests
    {
        [Fact]
        public void CreditacoesService_GetCardStatus_ShouldGetCardStatusAndCreateOneCard()
        {
            var logMock = new Mock<ILog>();
            var httprequestMock = new Mock<IHttpRequest>();
            httprequestMock.Setup(x => x.CardStateAsync(It.IsAny<string>())).Returns(Task.FromResult(true));
            ISagaService creditacoesService = new SagaService(logMock.Object, httprequestMock.Object);
            bool result = creditacoesService.GetCardState("12");
            Assert.True(result);
        }

        [Fact]
        public void CreditacoesService_GetCardStatus_ShouldGetCardStatusAndFailCreatingaCard()
        {
            var logMock = new Mock<ILog>();
            var httprequestMock = new Mock<IHttpRequest>();
            httprequestMock.Setup(x => x.CardStateAsync(It.IsAny<string>())).Returns(Task.FromResult(false));
            httprequestMock.Setup(x => x.CardPostAsync(It.IsAny<CardDto>())).Returns(Task.FromResult(""));
            ISagaService creditacoesService = new SagaService(logMock.Object, httprequestMock.Object);
            bool result = creditacoesService.GetCardState("12");
            Assert.False(result);
        }


        [Fact]
        public void CreditacoesService_GetCardStatus_ShouldGetCardStatusAndFail()
        {
            var logMock = new Mock<ILog>();
            var httprequestMock = new Mock<IHttpRequest>();
            httprequestMock.Setup(x => x.CardStateAsync(It.IsAny<string>())).Returns(Task.FromResult(false));
            ISagaService creditacoesService = new SagaService(logMock.Object, httprequestMock.Object);
            bool result = creditacoesService.GetCardState("12");
            Assert.False(result);
        }

        [Fact]
        public void CreditacoesService_PostCard_ShouldFailToPostTheCard()
        {
            var list = new List<string>();
            list.Add("google.pt");
            var logMock = new Mock<ILog>();
            var httprequestMock = new Mock<IHttpRequest>();
            httprequestMock.Setup(x => x.CardPostAsync(It.IsAny<CardDto>())).Returns(Task.FromResult(string.Empty));
            ISagaService creditacoesService = new SagaService(logMock.Object, httprequestMock.Object);
            string result = creditacoesService.PostNewCard(new CardDto("Carlos Campos", DateTime.Now.AddDays(1), "Carlos Campos", 1, list,
                "ISMAI", "Informática", "Carlos Campos", false));
            Assert.Empty(result);
        }

        [Fact]
        public void CreditacoesService_PostCard_ShouldPostTheCard()
        {
            var list = new List<string>();
            list.Add("google.pt");
            var logMock = new Mock<ILog>();
            var httprequestMock = new Mock<IHttpRequest>();
            httprequestMock.Setup(x => x.CardPostAsync(It.IsAny<CardDto>())).Returns(Task.FromResult(Guid.NewGuid().ToString()));
            ISagaService creditacoesService = new SagaService(logMock.Object, httprequestMock.Object);
            string result = creditacoesService.PostNewCard(new CardDto("Carlos Campos", DateTime.Now.AddDays(1), "Carlos Campos", 1, list,
                "ISMAI", "Informática", "Carlos Campos", false));
            Assert.NotEmpty(result);
        }

        [Fact]
        public void CreditacoesService_GetCardAttchments_ShouldGetCardAttachments()
        {
            var list = new List<string>();
            list.Add("google.pt");
            var logMock = new Mock<ILog>();
            var httprequestMock = new Mock<IHttpRequest>();
            httprequestMock.Setup(x => x.GetCardAttachments(It.IsAny<string>()))
                .Returns(Task.FromResult(list));
            ISagaService creditacoesService = new SagaService(logMock.Object, httprequestMock.Object);
            List<string> result = creditacoesService.GetCardAttachments("12");
            Assert.True(result.Count > 0);
        }
    }
}