namespace ClinicVet.PetManager.Job.Infra.Data.Oracle.Builder.v1;

public static class DeleteAgendaAndPetDataBuilder
{
    public static string DeleteAgendaAndPetData =
        @"BEGIN
             DELETE FROM CLINICVET.TBL_AGENDA a
             WHERE EXISTS (
             SELECT 1
             FROM CLINICVET.TBL_PET p
             JOIN CLINICVET.TBL_PET_OWNER o
                ON p.OWNER_ID = o.OWNER_ID
             WHERE o.OWNER_TYPE = :ownerType
                AND p.PET_ID = a.PET_ID );

             DELETE FROM CLINICVET.TBL_PET p
             WHERE EXISTS (
             SELECT 1
             FROM CLINICVET.TBL_PET_OWNER o
             WHERE o.OWNER_TYPE = :ownerType
                AND o.OWNER_ID = p.OWNER_ID);
        END;";
}