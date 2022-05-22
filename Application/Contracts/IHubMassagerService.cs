namespace Application.Contracts;

public interface IHubMassagerService
{
    public Task SendGroupNotify(string groupId, string notify);
}