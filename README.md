# Api de Vendas

Controle de Vendas, responsável por gerenciar e registrar todas as transações de vendas de uma empresa.

## 🚀 Começando

Essas instruções permitirão que você obtenha uma cópia do projeto em operação na sua máquina local para fins de desenvolvimento e teste.

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

Após as instalação do Docker Desktop

Copie e cole o comando abaixo no terminal

```
docker run -d -e MONGO_INITDB_ROOT_USERNAME=adm -e MONGO_INITDB_ROOT_PASSWORD=123 -p 27017:27017 --name meu-mongo mongo
```
Observe que estou configurando o usuário *adm* com a senha *123*. e também a porta 27017. Fica a seu critério alterar essas informações, se desejar

Copie e cole o comando abaixo no termina

````
docker ps
````
Esse comando permite visualizar todas as imagens criadas no Docker.

Copie e cole o comando abaixo no termina

```
docker exec -it meu-mongo mongosh -u adm -p 123
```
Esse comando executa o shell do MongoDB. Não é necessário realizar nenhuma ação adicional, basta copiar a ConnectionString gerada.

 - <em>É semelhante ao texto abaixo:</em>
 **<h6>mongodb://<credentials>@127.0.0.1:27017/?directConnection=true&serverSelectionTimeoutMS=2000&appName=mongosh+2.3.1</h6>**


## ⚙️ Executando os testes

Explicar como executar os testes automatizados para este sistema.

### 🔩 Analise os testes de ponta a ponta

Explique que eles verificam esses testes e porquê.

```
Dar exemplos
```

### ⌨️ E testes de estilo de codificação

Explique que eles verificam esses testes e porquê.

```
Dar exemplos
```

## 📦 Implantação

Adicione notas adicionais sobre como implantar isso em um sistema ativo

## 🛠️ Construído com

Mencione as ferramentas que você usou para criar seu projeto

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - O framework web usado
* [Maven](https://maven.apache.org/) - Gerente de Dependência
* [ROME](https://rometools.github.io/rome/) - Usada para gerar RSS

## 🖇️ Colaborando

Por favor, leia o [COLABORACAO.md](https://gist.github.com/usuario/linkParaInfoSobreContribuicoes) para obter detalhes sobre o nosso código de conduta e o processo para nos enviar pedidos de solicitação.

## 📌 Versão

Nós usamos [SemVer](http://semver.org/) para controle de versão. Para as versões disponíveis, observe as [tags neste repositório](https://github.com/suas/tags/do/projeto). 

## ✒️ Autores

Mencione todos aqueles que ajudaram a levantar o projeto desde o seu início

* **Um desenvolvedor** - *Trabalho Inicial* - [umdesenvolvedor](https://github.com/linkParaPerfil)
* **Fulano De Tal** - *Documentação* - [fulanodetal](https://github.com/linkParaPerfil)

Você também pode ver a lista de todos os [colaboradores](https://github.com/usuario/projeto/colaboradores) que participaram deste projeto.

## 📄 Licença

Este projeto está sob a licença Marco - veja o arquivo [LICENSE.md](https://github.com/usuario/projeto/licenca) para detalhes.


---
### ❤️ 😊 ⌨️ 