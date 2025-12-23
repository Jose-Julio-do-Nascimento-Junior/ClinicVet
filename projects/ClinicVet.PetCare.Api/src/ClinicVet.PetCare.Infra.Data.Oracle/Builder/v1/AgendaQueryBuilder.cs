using ClinicVet.PetCare.Domain.Resources.v1;

namespace ClinicVet.PetCare.Infra.Data.Oracle.Builder.v1;

public static class AgendaQueryBuilder
{
    public const string GetAgendas = @"PKG_AGENDA.GET_AGENDAS";

    public const string CreateAgenda = @"PKG_AGENDA.INSERT_AGENDA";

    public const string UpdateAgenda = @"PKG_AGENDA.UPDATE_AGENDA";

    public const string GetTemporaryAgendas =
        @"SELECT
              TO_CHAR(a.APPOINTMENT_AT, 'DD/MM/YYYY HH24:MI') AS ""AppointmentAt"",
              a.REASON AS ""Reason"",
              a.STATUS AS ""AgendaStatus"",
              a.PET_NAME AS ""PetName"",
              a.PET_SPECIES AS ""PetSpecie"",
              a.OWNER_PHONE AS ""OwnerPhone"",
              a.OWNER_TYPE AS ""OwnerType"",
              a.OWNER_DOCUMENT AS ""OwnerDocument"",
              a.DOC_TYPE AS ""DocType""
          FROM CLINICVET.TBL_AGENDA_TEMP a
          WHERE (:document IS NULL OR a.OWNER_DOCUMENT = :document)
          ORDER BY a.APPOINTMENT_AT
          OFFSET :offset ROWS
          FETCH NEXT :limit ROWS ONLY";

}