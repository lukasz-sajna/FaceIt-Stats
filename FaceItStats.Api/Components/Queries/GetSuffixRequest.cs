namespace FaceItStats.Api.Components.Queries
{
    using MediatR;

    public class GetSuffixRequest : IRequest<string>
    {
        public GetSuffixRequest(int counter)
        {
            Counter = counter;
        }

        public int Counter { get; }
    }
}
