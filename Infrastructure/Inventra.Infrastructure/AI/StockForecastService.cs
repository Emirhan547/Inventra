using Inventra.Application.Abstractions.Infrastructures.AI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.AI
{
    public sealed class StockForecastService
     : IStockForecastService
    {
        public int PredictNext30Days(
            List<float> history)
        {
            if (history.Count == 0)
            {
                return 0;
            }

            var averageDailyUsage =
                history.Average();

            return
                (int)Math.Ceiling(
                    averageDailyUsage * 30);
        }
    }
}