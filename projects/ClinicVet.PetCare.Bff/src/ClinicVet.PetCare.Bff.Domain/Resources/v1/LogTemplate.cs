namespace ClinicVet.PetCare.Domain.Resources.v1;

public static class LogTemplate
{
    public const string StartHandler = "[Iniciando] handler: {handler}.";

    public const string EndHandler = "[Finalizando] handler: {handler}. | {message} ";

    public const string TraceHandler = "[Executando] -> {message} | commandHandler: {handler}, method: {method}.";
}