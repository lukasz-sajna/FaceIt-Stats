using System.Threading.Tasks;

namespace FaceItStats.Api.Hubs
{
    public interface IFaceItStatsHub
    {
        Task NotifyFaceItStatsChanged(string method);
    }
}