using Bot.Utilities.Processed.Packet;
using static Bot.Utilities.Processed.Packet.Player;

namespace Bot.Utilities.Processed.BallPrediction
{
    public struct PredictionSlice
    {
        public Physics Physics;
        public float GameSeconds;

        public PredictionSlice(rlbot.flat.PredictionSlice predictionSlice)
        {
            Physics = new Physics(physics: predictionSlice.Physics.Value);
            GameSeconds = predictionSlice.GameSeconds;
        }
    }
}