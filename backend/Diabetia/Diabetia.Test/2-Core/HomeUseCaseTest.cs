using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FakeItEasy;
using Diabetia.Application.UseCases;
using Diabetia.Common.Utilities;
using Diabetia.Domain.Services;

namespace Diabetia.Test._2_Core
{
    public class HomeUseCaseTest
    {
        [Fact]
        public async Task ShowMetrics_Returns_Correct_Metrics()
        {
            var FakeHomeRepository = A.Fake<IHomeRepository>();
            var HomeUseCase = new HomeUseCase(FakeHomeRepository);

            string email = "example@example.com";

            A.CallTo(() => FakeHomeRepository.GetPhysicalActivity(email, (int)TypeEventEnum.ACTIVIDADFISICA)).Returns(10);
            A.CallTo(() => FakeHomeRepository.GetChMetrics(email, (int)TypeEventEnum.COMIDA)).Returns(150);
            A.CallTo(() => FakeHomeRepository.GetGlucose(email, (int)TypeEventEnum.GLUCOSA)).Returns(120);
            A.CallTo(() => FakeHomeRepository.GetHyperglycemia(email)).Returns(5);
            A.CallTo(() => FakeHomeRepository.GetHypoglycemia(email)).Returns(2);
            A.CallTo(() => FakeHomeRepository.GetInsulin(email, (int)TypeEventEnum.INSULINA)).Returns(20);

            var metrics = await HomeUseCase.ShowMetrics(email);

            Assert.Equal(10, metrics.PhysicalActivity);
            Assert.Equal(150, metrics.Carbohydrates);
            Assert.Equal(120, metrics.Glycemia);
            Assert.Equal(5, metrics.Hyperglycemia);
            Assert.Equal(2, metrics.Hypoglycemia);
            Assert.Equal(20, metrics.Insulin);
        }
    }
}
