using MediatR;
namespace FaceItStats.Api.Components.Commands
{
    public class SetBetStateRequest : IRequest
    {
        public SetBetStateRequest(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        public bool IsEnabled { get; }
    }
}
