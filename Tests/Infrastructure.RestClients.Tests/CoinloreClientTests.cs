namespace Infrastructure.RestClients.Tests
{
    using Infrastructure.RestClients.Interfaces;

    public class Tests
    {
        private ICoinloreClient _coinloreClient;
        [SetUp]
        public void Setup()
        {
            _coinloreClient = new CoinloreClient();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}