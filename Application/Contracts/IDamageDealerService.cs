using Application.Models;

namespace Application.Contracts;

public interface IDamageDealerService
{
    public Task DealDamage(UserHero userHero, Tuple<int, int> damageRange, string groupName);
}