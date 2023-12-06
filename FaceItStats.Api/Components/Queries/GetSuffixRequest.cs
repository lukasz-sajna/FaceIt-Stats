namespace FaceItStats.Api.Components.Queries
{
    using MediatR;

    public class GetSuffixRequest(int counter) : IRequest<string>
    {
        public int Counter { get; } = counter;
    }
}
