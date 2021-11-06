using Microsoft.VisualStudio.TestTools.UnitTesting;
using CanWeFixItApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanWeFixItService;
using System.Net.Http;
using System.Text.Json;

namespace CanWeFixItApi.Controllers.Tests
{
    [TestClass()]
    public class ControllerTests
    {
        private static readonly HttpClient Client = new();

        [TestMethod()]
        public async Task InstrumentControllerTest()
        {
            IList<Instrument> apiResult = (await Client.GetStringAsync("http://localhost:5010/v1/instruments/"))
                   .ParseJson<IList<Instrument>>();

            Assert.AreEqual(4, apiResult.Count);
            Assert.IsTrue(apiResult.All(x => x.Active));
            Assert.IsTrue(apiResult.All(x => x.Id == 2 || x.Id == 4 || x.Id == 6 || x.Id == 8));
        }

        [TestMethod()]
        public async Task MarketDataControllerTest()
        {
            IList<MarketDataDto> apiResult = (await Client.GetStringAsync("http://localhost:5010/v1/marketdata/"))
                    .ParseJson<IList<MarketDataDto>>();

            Assert.AreEqual(2, apiResult.Count);
            Assert.IsTrue(apiResult.All(x => x.Active));
            Assert.IsTrue(apiResult.All(x => x.Id == 2 || x.Id == 4) && (apiResult.All(x => x.Id == 2 || x.Id == 4)));
        }

        [TestMethod()]
        public async Task ValuationsControllerTest()
        {
            IList<MarketValuation> apiResult = (await Client.GetStringAsync("http://localhost:5010/v1/valuations/"))
                     .ParseJson<IList<MarketValuation>>();

            Assert.AreEqual(1, apiResult.Count);
            Assert.IsTrue(apiResult.First().Name == "DataValueTotal" && apiResult.First().Total == 13332);
        }
    }
    public static class Core
    {
        public static T ParseJson<T>(this string content)
        {
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
    }
}