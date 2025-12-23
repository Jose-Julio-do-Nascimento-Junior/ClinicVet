namespace ClinicVet.AgendaStatus.Job.Domain.Resources.v1;

public static class LogTemplate
{
    public const string StartHandler = "[Iniciando] handler: {handler}.";

    public const string EndHandler = "[Finalizando] handler: {handler}. | {message} ";

    public const string ErrorCommandHandler = "[Erro]: CommandHandler: {handler} | mensagem: {message}.";

    public const string ErrorHandler = "[Finalizando com Erro] handler: {handle}. | [Error] : {message} ";

    public const string StartDbQuery = "[Iniciando] query: {identifier}.";

    public const string EndDbQuery = "[Finalizando] query: {identifier}.";
}