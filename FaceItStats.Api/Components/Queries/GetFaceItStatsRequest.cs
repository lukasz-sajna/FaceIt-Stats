using FaceItStats.Api.Models;
using MediatR;

namespace FaceItStats.Api.Components.Queries
{
    public class GetFaceItStatsRequest(string nickname) : IRequest<FaceItStatsResponse>
    {
        public string Nickname { get; } = nickname;
    }
}
