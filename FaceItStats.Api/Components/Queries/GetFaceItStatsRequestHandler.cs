﻿using FaceItStats.Api.Client;
using FaceItStats.Api.Client.Models;
using FaceItStats.Api.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FaceItStats.Api.Components.Queries
{
    public class GetFaceItStatsRequestHandler : IRequestHandler<GetFaceItStatsRequest, FaceItStatsResponse>
    {
        private readonly FaceItStatsClient _faceItClient;

        public GetFaceItStatsRequestHandler()
        {
            _faceItClient = new FaceItStatsClient();
        }

        public async Task<FaceItStatsResponse> Handle(GetFaceItStatsRequest request, CancellationToken cancellationToken)
        {
            var stats = await _faceItClient.GetStatsForNickname(request.Nickname, cancellationToken);
            var userInfo = await _faceItClient.GetUserInfoForNickname(request.Nickname, cancellationToken);

            return stats.ToFaceItStatsResponse(userInfo.PlayerId);
        }
    }
}