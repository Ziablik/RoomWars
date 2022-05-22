using Application.Contracts;
using Application.Models;

namespace Infrastructure.Services;

public class DamageDealerService : IDamageDealerService
{
    private readonly IRandomWrapper _randomWrapper;
    private readonly IHubMassagerService _hubMassager;

    public DamageDealerService(IRandomWrapper randomWrapper, IHubMassagerService hubMassager)
    {
        _randomWrapper = randomWrapper;
        _hubMassager = hubMassager;
    }

    public async Task DealDamage(UserHero userHero, Tuple<int, int> damageRange, string groupName)
    {
        var (minDamage, maxDamage) = damageRange;
        var damageFromRange = _randomWrapper.RandomFromRange(minDamage, maxDamage);
        userHero.Health -= damageFromRange;

        await _hubMassager.SendGroupNotify(
            groupName,
            $"{userHero.Username} received {damageFromRange} damage. Health = {userHero.Health}"
        );
    }
}