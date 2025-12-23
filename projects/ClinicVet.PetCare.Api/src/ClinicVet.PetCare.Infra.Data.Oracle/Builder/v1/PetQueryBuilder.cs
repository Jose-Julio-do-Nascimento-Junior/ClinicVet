namespace ClinicVet.PetCare.Infra.Data.Oracle.Builder.v1;

public static class PetQueryBuilder
{
    public const string GetPets = @"PKG_PET.GET_PETS_BY_OWNER";

    public const string CreatePet = @"PKG_PET.INSERT_PET";

    public const string UpdatePet = @"PKG_PET.UPDATE_PET";

    public const string GetTemporaryPets =
        @"SELECT
             p.NAME AS ""PetName"",
             p.SPECIES AS ""PetSpecie"",
             p.BREED AS ""PetBreed"",
             p.BIRTH_DATE AS ""PetBirthDate"",
             p.OWNER_DOCUMENT AS ""OwnerDocument"",
             p.DOC_TYPE AS ""DocType""
          FROM CLINICVET.TBL_PET_TEMP p
          WHERE (:document IS NULL OR p.OWNER_DOCUMENT = :document)
          ORDER BY p.NAME
          OFFSET :offset ROWS
          FETCH NEXT :limit ROWS ONLY";
}