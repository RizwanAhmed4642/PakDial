using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PAKDial.Presentation.JobScheduling
{
    public class AutoUpdateExpiredOrders : AutoJobService
    {
        private readonly ILogger<AutoUpdateExpiredOrders> _logger;
        //private readonly IListingPremiumService _listingPremium;
        private readonly IServiceScopeFactory _scopeFactory;

        public AutoUpdateExpiredOrders(IScheduleConfig<AutoUpdateExpiredOrders> config, ILogger<AutoUpdateExpiredOrders> logger
            , IServiceScopeFactory scopeFactory)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            //_listingPremium = listingPremium;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto update job expired orders started.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Auto update job expired orders is in process.");
            using (var scope = _scopeFactory.CreateScope())
            {
                var listingPremium = scope.ServiceProvider.GetRequiredService<IListingPremiumService>();
                var GetExpiredRecords = listingPremium.AutoUpdateExpiredOrders();
                if (GetExpiredRecords.Count() > 0)
                {
                }
            }
            //var GetExpiredRecords = _listingPremium.AutoUpdateExpiredOrders();
            //if(GetExpiredRecords.Count() > 0)
            //{

            //}
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto update job expired orders ended.");
            return base.StopAsync(cancellationToken);
        }
    }
}
