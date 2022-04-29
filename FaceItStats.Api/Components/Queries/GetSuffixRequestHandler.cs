namespace FaceItStats.Api.Components.Queries
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetSuffixRequestHandler : IRequestHandler<GetSuffixRequest, string>
    {
        public Task<string> Handle(GetSuffixRequest request, CancellationToken cancellationToken)
        {
            var counterString = request.Counter.ToString();

            var result = string.Empty;

            switch (int.Parse(counterString.Substring(counterString.Length - 1)))
            {
                case 2:
                case 3:
                case 4:
                    result = "i";
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 0:
                    result = "ów";
                    break;
                default:
                    break;
            }

            return Task.FromResult(result);
        }
    }
}
