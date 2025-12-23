# ClinicVet.AgendaStatus.Job

CronJob responsável por atualizar o status de consultas agendadas no sistema ClinicVet.
<p>No final de cada expediente, o job identifica todos os agendamentos do dia que não foram atendidos e atualiza automaticamente seu status para Ausente.</p>

## Requisitos Funcionais

- Identificar agendamentos com data do dia atual;

- Verificar se o cliente não compareceu ao atendimento;

- Atualizar o status da agenda para Ausente ao final do expediente;

- Registrar logs de execuções, atualizações e possíveis falhas;

- Garantir a consistência dos dados durante o processamento.

## Tecnologias Utilizadas

**Backend:**

- .NET 8
- C# 12
- ASP.NET Core

**DataBase:**

- Oracle Database

## Atributos de Qualidade

- Simplicidade de operação e gerenciamento;

- Alta disponibilidade e performance no processamento diário;

- Segurança e integridade dos dados;

- Manutenibilidade e extensibilidade para novos status e regras de negócio.