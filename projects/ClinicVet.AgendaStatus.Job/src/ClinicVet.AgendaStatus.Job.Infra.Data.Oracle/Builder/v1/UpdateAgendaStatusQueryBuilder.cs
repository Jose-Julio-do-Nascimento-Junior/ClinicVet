namespace ClinicVet.AgendaStatus.Job.Infra.Data.Oracle.Builder.v1;

public static class UpdateAgendaStatusQueryBuilder
{
    public const string UpdateAgendaStatus =
        $@"UPDATE TBL_AGENDA
             SET STATUS = :absent
           WHERE STATUS = :scheduled
             AND TRUNC(APPOINTMENT_AT) = TRUNC(SYSDATE)
             AND APPOINTMENT_AT < SYSDATE";
}