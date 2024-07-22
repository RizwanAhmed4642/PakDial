using Microsoft.Extensions.Logging;
using PAKDial.Interfaces.PakDialServices.ICompaniesListingsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PAKDial.Presentation.JobScheduling
{
    public class AutoDeleteUnVerfiedRatings : AutoJobService
    {
        private readonly ILogger<AutoDeleteUnVerfiedRatings> _logger;
        private readonly ICompanyListingRatingService _ratingService;

        public AutoDeleteUnVerfiedRatings(IScheduleConfig<AutoDeleteUnVerfiedRatings> config, ILogger<AutoDeleteUnVerfiedRatings> logger,
            ICompanyListingRatingService ratingService)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _ratingService = ratingService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto delete job unverified rating started.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Auto delete job unverified rating is in process.");
            _ratingService.AutoDeleteUnVerfiedRating();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Auto delete job unverified rating ended.");
            return base.StopAsync(cancellationToken);
        }
    }
}
