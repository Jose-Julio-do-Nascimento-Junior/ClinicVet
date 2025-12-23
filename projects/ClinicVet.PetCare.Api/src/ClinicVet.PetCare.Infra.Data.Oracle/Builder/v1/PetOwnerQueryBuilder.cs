namespace ClinicVet.PetCare.Infra.Data.Oracle.Builder.v1;

public static class PetOwnerQueryBuilder
{
    public const string GetPetOwners = @"PKG_PET_OWNER.GET_PET_OWNERS";

    public const string CreatePetOwner = @"PKG_PET_OWNER.INSERT_PET_OWNER";

    public const string UpdatePetOwner = @"PKG_PET_OWNER.UPDATE_PET_OWNER";
}