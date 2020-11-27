﻿namespace EducationHub.Services.Data.Votes
{
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVotes(string userId)
            => this.votesRepository
                .AllAsNoTracking()
                .Where(v => v.VotedForId == userId)
                .Average(x => x.Value);

        public async Task SetVoteAsync(string votedId, string votedForId, byte value)
        {
            if (votedId == votedForId)
            {
                return;
            }

            var vote = await this.votesRepository
                .All()
                .FirstOrDefaultAsync(x => x.VotedId == votedId && x.VotedForId == votedForId);

            if (vote == null)
            {
                vote = new Vote
                {
                    VotedId = votedId,
                    VotedForId = votedForId,
                };
                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;

            await this.votesRepository.SaveChangesAsync();
        }
    }
}
