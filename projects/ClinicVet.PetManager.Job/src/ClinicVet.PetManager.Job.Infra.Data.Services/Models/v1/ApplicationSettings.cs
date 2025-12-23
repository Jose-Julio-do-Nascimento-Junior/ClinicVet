namespace ClinicVet.PetManager.Job.Infra.Data.Services.Models.v1;

public sealed class ApplicationSettings
{
    public static string SessionName => "Core:ApplicationSettings:Schedule";

    public string Schedule { get; set; } = string.Empty;

    public static string PetManageTrigger => "PetManageTrigger";

    public static string JobKey => " PetManagerWorkerHandlerKey";
}