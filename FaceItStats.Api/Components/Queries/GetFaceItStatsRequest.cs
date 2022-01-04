using FaceItStats.Api.Models;
using MediatR;

namespace FaceItStats.Api.Components.Queries
{
    public class GetFaceItStatsRequest : IRequest<FaceItStatsResponse>
    {
        public GetFaceItStatsRequest(string nickname)
        {
            Nickname = nickname;
        }

        public string Nickname { get; }
    }
}
