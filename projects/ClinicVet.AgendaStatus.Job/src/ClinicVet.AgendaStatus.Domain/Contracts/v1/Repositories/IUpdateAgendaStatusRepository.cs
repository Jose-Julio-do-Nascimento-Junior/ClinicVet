namespace ClinicVet.AgendaStatus.Job.Domain.Contracts.v1.Repositories;

public interface IUpdateAgendaStatusRepository
{
    Task UpdateAgendaStatusAsync(CancellationToken cancellationToken);
}