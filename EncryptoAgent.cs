
using rlbot.flat;
using RLBotDotNet;

namespace EncryptoBot
{
	class EncryptoAgent : RLBotDotNet.Bot
	{
		public EncryptoAgent(string botName, int botTeam, int botIndex) : base(botName, botTeam, botIndex)
		{
			botName = "testName";
		}
		public override Controller GetOutput(GameTickPacket gameTickPacket)
		{
			return new Controller();
		}
	}
}
