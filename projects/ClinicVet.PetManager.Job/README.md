# ClinicVet.PetManager.Job

Esse cronjob é responsável por realizar a limpeza e reorganização periódica dos dados de agendamentos e pets no sistema ClinicVet.
<p>Após um período determinado, o processo identifica os tutores temporários e, a partir deles, localiza seus respectivos pets e agendamentos, removendo-os das tabelas principais e migrando-os para tabelas temporárias.</p>

## Requisitos Funcionais

- Identificar tutores classificados como temporários após o período configurado;

- Migrar pets temporários para a tabela TBL_PET_TEMP;

- Migrar agendamentos temporários para a tabela TBL_AGENDA_TEMP;

- Garantir que nas tabelas principais (TBL_PET, TBL_AGENDA) permaneçam apenas ONGs e tutores permanentes;

- Registrar logs de execução e possíveis falhas no processo de migração.

## Tecnologias Utilizadas

**Backend:**

- .NET 8
- C# 12
- ASP.NET Core

**DataBase:**

- Oracle Database

## Atributos de Qualidade

- Simplicidade de operação e gerenciamento;

- Integridade e consistência de dados durante o processo de migração;

- Performance adequada para execução periódica;

- Segurança no tratamento e movimentação dos registros.