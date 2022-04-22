namespace FaceItStats.Api.Components.Commands
{
    using FaceItStats.Api.Models;
    using MediatR;

    public class UpdateChallangeDataRequest : IRequest
    {
        public UpdateChallangeDataRequest(ChallangeData challangeData)
        {
            this.challange = challangeData;
        }

        public ChallangeData challange { get; private set; }
    }
}
