namespace ClinicVet.PetManager.Job.Infra.Data.Oracle.Builder.v1;

public static class AgendaManageQueryBuilder
{
    public static string AgendaManager =
      @" INSERT INTO CLINICVET.TBL_AGENDA_TEMP (
            AGENDA_ID,
            APPOINTMENT_AT,
            REASON,
            STATUS,
            PET_ID,
            OWNER_ID,
            PET_NAME,
            PET_SPECIES,
            OWNER_PHONE,
            OWNER_TYPE,
            OWNER_DOCUMENT,
            DOC_TYPE)
         SELECT
            a.AGENDA_ID,
            a.APPOINTMENT_AT,
            a.REASON,
            a.STATUS,
            a.PET_ID,
            a.OWNER_ID,
            a.PET_NAME,
            a.PET_SPECIES,
            a.OWNER_PHONE,
            a.OWNER_TYPE,
            a.OWNER_DOCUMENT,
            a.DOC_TYPE
         FROM CLINICVET.TBL_AGENDA a
         JOIN CLINICVET.TBL_PET_OWNER o
            ON a.OWNER_ID = o.OWNER_ID
         WHERE o.OWNER_TYPE = :ownerType
            AND NOT EXISTS(
         SELECT 1
         FROM CLINICVET.TBL_AGENDA_TEMP t
         WHERE t.AGENDA_ID = a.AGENDA_ID)";
}