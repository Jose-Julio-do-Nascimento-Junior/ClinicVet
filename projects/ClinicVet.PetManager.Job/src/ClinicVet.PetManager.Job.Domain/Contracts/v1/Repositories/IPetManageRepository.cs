namespace ClinicVet.PetManager.Job.Domain.Contracts.v1.Repositories;

public interface IPetManageRepository
{
    Task<int> PetManagerAsync(CancellationToken cancellationToken);
}