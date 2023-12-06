using MediatR;
namespace FaceItStats.Api.Components.Commands
{
    public class SetBetStateRequest(bool isEnabled) : IRequest
    {
        public bool IsEnabled { get; } = isEnabled;
    }
}
