using Grpc.Net.Client;
using MLNetService;
using System;
using System.Threading.Tasks;

namespace MLNet_gRPC_Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Prediction.PredictionClient(channel);

            var request = new HousingRequest
            {
                Longitude = -122.25f,
                Latitude = 37.85f,
                HousingMedianAge = 55.0f,
                TotalRooms = 1627.0f,
                TotalBedrooms = 235.0f,
                Population = 322.0f,
                Households = 120.0f,
                MedianIncome = 8.3014f,
                OceanProximity = "NEAR BAY"
            };

            var response = await client.PredictAsync(request);

            Console.WriteLine($"Prediction is {response.Score.ToString("c")}");

            Console.ReadLine();
        }
    }
}
