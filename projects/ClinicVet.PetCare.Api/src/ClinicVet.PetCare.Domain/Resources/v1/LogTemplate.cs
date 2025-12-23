namespace ClinicVet.PetCare.Domain.Resources.v1;

public static class LogTemplate
{
    public const string StartHandler = "[Iniciando] handler: {handler}.";

    public const string EndHandler = "[Finalizando] handler: {handler}. | {message} ";

    public const string TraceHandler = "[Executando] -> {message} | commandHandler: {handler}, method: {method}.";

    public const string ErrorHandler = "[Finalizando] handler: {handler}. | [Error] : {message} ";

    public const string StartDbQuery = "[Iniciando] query: {identifier}.";

    public const string EndDbQuery = "[Finalizando] query: {identifier}.";

    public const string ErrorLegacy = "[Finalizando] handler: {handler}. | [Error] : {message} legacy code: {code}";
}