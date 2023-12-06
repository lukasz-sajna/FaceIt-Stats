namespace FaceItStats.Api.Components.Commands
{
    using Models;
    using MediatR;

    public class UpdateChallengeDataRequest(ChallengeData challengeData) : IRequest
    {
        public ChallengeData Challenge { get; private set; } = challengeData;
    }
}
