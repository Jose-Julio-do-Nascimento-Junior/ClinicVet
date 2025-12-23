namespace ClinicVet.PetManager.Job.Infra.Data.Oracle.Builder.v1;

public class PetManageQueryBuilder
{
    public static string PetManager =
      $@" INSERT INTO CLINICVET.TBL_PET_TEMP (
             PET_ID,
             NAME,
             SPECIES,
             BREED,
             BIRTH_DATE,
             OWNER_ID,
             OWNER_DOCUMENT,
             DOC_TYPE)
          SELECT
             p.PET_ID,
             p.NAME,
             p.SPECIES,
             p.BREED,
             p.BIRTH_DATE,
             p.OWNER_ID,
             p.OWNER_DOCUMENT,
             p.DOC_TYPE
          FROM CLINICVET.TBL_PET p
          JOIN CLINICVET.TBL_PET_OWNER o
             ON p.OWNER_ID = o.OWNER_ID
          WHERE o.OWNER_TYPE = :ownerType
             AND NOT EXISTS (
          SELECT 1
          FROM CLINICVET.TBL_PET_TEMP t
          WHERE t.PET_ID = p.PET_ID)";
}