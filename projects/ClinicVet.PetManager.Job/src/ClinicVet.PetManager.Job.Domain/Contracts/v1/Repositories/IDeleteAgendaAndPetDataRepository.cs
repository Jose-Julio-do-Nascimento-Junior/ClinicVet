namespace ClinicVet.PetManager.Job.Domain.Contracts.v1.Repositories;

public interface IDeleteAgendaAndPetDataRepository
{
    Task DeleteAgendaAndPetDataAsync(CancellationToken cancellationToken);
}