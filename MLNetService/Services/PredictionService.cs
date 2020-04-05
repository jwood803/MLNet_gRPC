using Grpc.Core;
using Microsoft.ML;
using System.Threading.Tasks;

namespace MLNetService.Services
{
    public class PredictionService : Prediction.PredictionBase
    {
        private readonly MLContext _context;
        private readonly PredictionEngine<HousingRequest, HousingReply> _predictionEngine;

        public PredictionService()
        {
            _context = new MLContext();

            var model = _context.Model.Load("./Model/housing-model.zip", out DataViewSchema inputSchema);

            _predictionEngine = _context.Model.CreatePredictionEngine<HousingRequest, HousingReply>(model, inputSchema: inputSchema);
        }

        public override async Task<HousingReply> Predict(HousingRequest request, ServerCallContext context)
        {
            var response = new HousingReply();

            var prediction = _predictionEngine.Predict(request);

            response.Score = prediction.Score;

            return await Task.FromResult(response);
        }
    }
}
