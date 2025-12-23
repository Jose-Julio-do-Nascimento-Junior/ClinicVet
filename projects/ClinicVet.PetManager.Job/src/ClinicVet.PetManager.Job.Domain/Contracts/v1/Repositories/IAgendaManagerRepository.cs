namespace ClinicVet.PetManager.Job.Domain.Contracts.v1.Repositories;

public interface IAgendaManagerRepository
{
    Task<int> AgendaManagerAsync(CancellationToken cancellationToken);
}