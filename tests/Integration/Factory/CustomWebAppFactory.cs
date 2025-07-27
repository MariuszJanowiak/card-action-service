namespace CardActionServiceTests.Integration.Factory;

public class CustomWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<ICardDataProvider>();

            var mock = new Mock<ICardDataProvider>();
            mock.Setup(p => p.GetCardDetailsAsync(It.IsAny<string>(), "4111111111111111"))
                .ReturnsAsync(new TestCardDetails(
                    "4111111111111111",
                    EnCardStatus.Active,
                    EnCardType.Credit,
                    true
                ));


            services.AddSingleton(mock.Object);
        });

        builder.ConfigureAppConfiguration((_, config) =>
        {
            var settings = new Dictionary<string, string?>
            {
                { "Security:ApiKey", "test-key" }
            };
            config.AddInMemoryCollection(settings);
        });
    }
}