using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Infrastructures.AI
{
    public interface IStockForecastService
    {
        int PredictNext30Days(
            List<float> history);
    }
}
