# ClinicVet.PetCare.Bff

O BFF (Backend for Frontend) atua como um proxy para a API ClinicVet, espelhando seus endpoints e fornecendo cache para otimizar a performance das consultas. 
<p>Serve apenas para centralizar o acesso aos dados, reduzir a carga na API principal e melhorar a latência das respostas para os clientes e front-end.</p>

## Requisitos Funcionais

- Encaminhar requisições do front-end para a API principal;

- Implementar cache para reduzir o número de chamadas à API e melhorar a performance;

- Garantir consistência dos dados refletidos para o front-end;

- Registrar logs de acesso e falhas.

## Tecnologias Utilizadas

**Backend:**

- .NET 8
- C# 12
- ASP.NET Core

**Cache:**
- Redis

## Atributos de Qualidade

- Simplicidade de operação e manutenção;

- Performance otimizada no acesso a dados;

- Escalabilidade para atender múltiplos clientes;

- Segurança nas requisições entre front-end e API.