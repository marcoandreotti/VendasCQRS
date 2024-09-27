# Api de Vendas (PoC)

Controle de Vendas, responsável por gerenciar e registrar todas as transações de vendas de uma empresa.

## 🚀 Começando

Essas instruções permitirão que você obtenha uma cópia do projeto em operação na sua máquina local para fins de desenvolvimento e teste.

---

### 📇 User Story (Exemplo)

Este é um esboço de uma [user story](Documentos/Historia_Vendas_Spr1.docx) para a execução desta tarefa no modelo Scrum.

---

### 📋 Pré-requisitos

- Docker
- Git

---

🎁<strong>Se não quiser instalar o Docker Desktop, você pode optar por utilizar o [MongoDB Atlas](https://www.mongodb.com/pt-br/cloud/atlas/register) para acessar o banco de dados na nuvem e rodar o projeto usando o [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/community/) ou [Visual Studio Code](https://code.visualstudio.com/download) (por meio de linhas de comando).</strong>

🎁<strong>Também se não quiser instalar o Git, você pode optar por fazer o [donwload do projeto clicando aqui](https://github.com/marcoandreotti/VendasCQRS/archive/refs/heads/main.zip).</strong>


---

### 🔧 Clonar o Projeto

Passo 1: Instale o [Git](https://git-scm.com/downloads) em sua máquina.

Passo 2: Após a instalação, abra ou crie uma pasta em um diretório de sua escolha. Em seguida, execute os seguintes comandos na ordem indicada:

````
git clone https://github.com/marcoandreotti/VendasCQRS.git
````

**Com este comando, você já poderá acessar o conteúdo do projeto baixado em sua máquina.**



### 🔧 Instalação

Instalar o [Docker Desktop](https://www.docker.com/products/docker-desktop/) em sua máquina e executar o contêiner do MongoDB diretamente através dele.

Após as instalação do Docker Desktop, abra o terminal e execute os camandos na ordem

```
docker run -d -e MONGO_INITDB_ROOT_USERNAME=adm -e MONGO_INITDB_ROOT_PASSWORD=123 -p 27017:27017 --name meu-mongo mongo
```
Observe que estou configurando o usuário *adm* com a senha *123*. e também a porta 27017. Fica a seu critério alterar essas informações, se desejar

````
docker ps
````
Esse comando permite visualizar todas as imagens criadas no Docker.

```
docker exec -it meu-mongo mongosh -u adm -p 123
```
Esse comando executa o shell do MongoDB. Não é necessário realizar nenhuma ação adicional, basta copiar a ConnectionString gerada.

 - <em>É semelhante ao texto abaixo:</em>
 **<h6>mongodb://adm:123@127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&appName=mongosh+2.3.1</h6>**


## 📦 Implantação

Vamos executar e depurar a solução no [Visual Studio Community](https://visualstudio.microsoft.com/pt-br/vs/community/), que oferece uma interface intuitiva para facilitar a execução.

Após a instalação, abra o Visual Studio e navegue até a pasta do projeto para localizar e abrir o arquivo **Sale.sln**

![img_vs_1](Documentos/imgs/vs_studio_explorer01.jpg)

Agora, basta selecionar o projeto Api e clicar no botão de 'Play' para iniciar a execução.

![img_vs_1](Documentos/imgs/vs_studio_explorer02.jpg)


se tudo ocorrer bem, o resultado será esse:

![swagger_01](Documentos/imgs/swagger-01.png)

## ⚙️ Executando os testes

Para executar os testes unitários no .NET 8, siga estas instruções:

Abra o terminal no diretório raiz do seu projeto.

Certifique-se de que o [.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) está instalado e configurado corretamente.

Execute o seguinte comando para rodar os testes unitários:

````
dotnet test
````
Esse comando irá descobrir e executar todos os testes unitários presentes no projeto de testes, fornecendo um relatório com os resultados diretamente no terminal.

Também é possível executar os testes diretamente no Visual Studio Community. Para isso, localize o projeto **Tests** no Solution Explorer, clique com o botão direito do mouse e selecione a opção **Run Tests** no menu.

![test_01](Documentos/imgs/tests-01.png)

## 🛠️ Construído com

Pacotes do .NET 8 utilizados:

[MediatR](https://www.nuget.org/packages/MediatR/): Implementa o padrão Mediator, permitindo desacoplar o envio de comandos e consultas (CQRS) em aplicações. Ele facilita a comunicação entre componentes sem dependências diretas.

[Serilog](https://www.nuget.org/packages/Serilog/4.0.2-dev-02226): Biblioteca de logging flexível e extensível, que permite registrar logs em vários destinos, como arquivos, bancos de dados e sistemas de monitoramento, com suporte a logs estruturados.

[AutoMapper](https://www.nuget.org/packages/AutoMapper): Ferramenta para mapear objetos de forma automática, facilitando a conversão de objetos de um tipo para outro (ex: de entidade para DTO), reduzindo o código repetitivo de mapeamento.

[FluentValidation](https://www.nuget.org/packages/FluentValidation): Biblioteca para validação de objetos que usa uma API fluente, facilitando a criação de regras de validação de dados de maneira expressiva e legível.

[MongoDB](https://www.nuget.org/packages/MongoDB.Bson): Pacote de driver oficial do .NET para trabalhar com o banco de dados NoSQL MongoDB, permitindo realizar operações CRUD e consultas.

[LinqKit](https://www.nuget.org/packages/LinqKit): Biblioteca que estende o LINQ, oferecendo suporte a expressões dinâmicas, predicados reutilizáveis e consultas avançadas, como filtros condicionais.

[Microsoft.Extensions.Configuration.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Abstractions/9.0.0-rc.1.24431.7): Define uma abstração para a configuração de aplicativos no .NET, permitindo a leitura de configurações de várias fontes, como arquivos JSON, variáveis de ambiente, etc.

[Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json): Biblioteca popular para serialização e desserialização de objetos JSON, amplamente utilizada por sua flexibilidade e facilidade de uso.

[coverlet.collector](https://www.nuget.org/packages/coverlet.collector): Ferramenta que coleta métricas de cobertura de código durante a execução de testes, integrando-se ao .NET para mostrar a porcentagem de código testado.

[FluentAssertions](https://www.nuget.org/packages/FluentAssertions/7.0.0-alpha.4): Biblioteca para facilitar a criação de asserções em testes unitários, permitindo escrever afirmações mais expressivas e legíveis.

[Microsoft.NET.Test.Sdk](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk): Biblioteca de suporte ao .NET para execução de testes unitários com ferramentas como xUnit, NUnit, ou MSTest.

[Moq](https://www.nuget.org/packages/Moq.AutoMock/3.6.0-ci0596): Framework de mocking para testes, usado para criar objetos simulados que imitam o comportamento de dependências de código, facilitando a execução de testes unitários.

[xUnit](https://www.nuget.org/packages/xunit): Framework popular de testes unitários para .NET, que permite escrever e executar testes automatizados, focando em simplicidade e extensibilidade.


## 📌 Versão

Nós usamos [SemVer](http://semver.org/) para controle de versão. Para as versões disponíveis, observe as [tags neste repositório](https://github.com/suas/tags/do/projeto). 

## ✒️ Autores

Aqueles que ajudaram a levantar esse projeto desde o seu início

* **Marco Antonio Andreotti** - *Trabalho Inicial* - [marco-andreotti](https://www.linkedin.com/in/marco-antonio-andreotti-3a040119/)

## 📄 Licença

Este projeto está sob a licença Marco Andreotti - veja o arquivo [LICENSE.md](LICENSE.md) para detalhes.
