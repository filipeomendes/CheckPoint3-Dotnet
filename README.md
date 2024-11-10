# Projeto: Cadastro de Livros e Previsão de Títulos com Machine Learning

**Turma:** 2TDSPV

**Integrantes:**
- Fernando Paparelli Aracena (rm551022)
- Filipe de Oliveira Mendes (rm98959)
- Miron Gonçalves Martins (rm551801)
- Victor Luca do Nascimento Queiroz (rm551886)
- Vinicius Pedro de Souza (rm550907)

## Descrição do Projeto

Este projeto tem como objetivo criar uma API Web para o cadastro de livros e bibliotecas, bem como implementar uma funcionalidade de previsão de título de livros utilizando técnicas de Machine Learning.

A API permite o gerenciamento de livros e a previsão automática de títulos com base em informações como autor, gênero, ano de publicação e classificação. A previsão é realizada através de um modelo de Machine Learning treinado com dados históricos de livros armazenados no banco de dados.

### Funcionalidades Principais

- **Cadastro de Livros:** Permite o cadastro de livros no banco de dados.
- **Atualização de Livros:** Permite a atualização de informações de um livro já existente.
- **Exclusão de Livros:** Permite a remoção de livros cadastrados.
- **Listagem de Livros:** Exibe todos os livros cadastrados.
- **Previsão de Títulos:** Utiliza Machine Learning para prever o título de um livro com base em informações fornecidas.

### Tecnologias Utilizadas

- **ASP.NET Core Web API:** Para a criação da API.
- **Entity Framework Core:** Para interação com o banco de dados.
- **Machine Learning (ML.NET):** Para a implementação do modelo de previsão de títulos.
- **JWT (JSON Web Tokens):** Para autenticação e autorização de usuários.

## Como Funciona

A API foi estruturada com as seguintes rotas e funcionalidades:

### Requisições da API

#### 1. Cadastro de Livros

- **POST** `/api/Livro`
  - **Descrição:** Cadastra um novo livro no banco de dados.
  - **Corpo da Requisição (JSON):**
    ```json
    {
      "titulo": "Título do Livro",
      "autor": "Autor do Livro",
      "genero": "Gênero do Livro",
      "anoPublicacao": 2024,
      "sinopse": "Sinopse do livro",
      "classificacao": 4.5
    }
    ```
  - **Resposta:** Retorna o livro cadastrado com o ID gerado.

#### 2. Listagem de Livros

- **GET** `/api/Livro`
  - **Descrição:** Retorna a lista de todos os livros cadastrados.
  - **Resposta (JSON):**
    ```json
    [
      {
        "id": 1,
        "titulo": "Título do Livro",
        "autor": "Autor do Livro",
        "genero": "Gênero",
        "anoPublicacao": 2024,
        "sinopse": "Sinopse do livro",
        "classificacao": 4.5
      },
      ...
    ]
    ```

#### 3. Detalhes de um Livro

- **GET** `/api/Livro/{id}`
  - **Descrição:** Retorna os detalhes de um livro específico.
  - **Parâmetros:** `id` (ID do livro).
  - **Resposta (JSON):**
    ```json
    {
      "id": 1,
      "titulo": "Título do Livro",
      "autor": "Autor do Livro",
      "genero": "Gênero",
      "anoPublicacao": 2024,
      "sinopse": "Sinopse do livro",
      "classificacao": 4.5
    }
    ```

#### 4. Atualização de um Livro

- **PUT** `/api/Livro/{id}`
  - **Descrição:** Atualiza as informações de um livro existente.
  - **Parâmetros:** `id` (ID do livro).
  - **Corpo da Requisição (JSON):**
    ```json
    {
      "titulo": "Novo Título",
      "autor": "Novo Autor",
      "genero": "Novo Gênero",
      "anoPublicacao": 2024,
      "sinopse": "Nova Sinopse",
      "classificacao": 5
    }
    ```
  - **Resposta:** Retorna status 204 (No Content) caso a atualização seja bem-sucedida.

#### 5. Exclusão de um Livro

- **DELETE** `/api/Livro/{id}`
  - **Descrição:** Exclui um livro do banco de dados.
  - **Parâmetros:** `id` (ID do livro).
  - **Resposta:** Retorna status 204 (No Content) caso a exclusão seja bem-sucedida.

#### 6. Previsão de Título de Livro

- **POST** `/api/PrevisaoLivro/prever`
  - **Descrição:** Preve o título de um livro com base nas informações fornecidas.
  - **Corpo da Requisição (JSON):**
    ```json
    {
      "titulo": "Título do Livro",
      "autor": "Autor do Livro",
      "genero": "Gênero",
      "anoPublicacao": 2024,
      "classificacao": 4.5
    }
    ```
  - **Resposta (JSON):**
    ```json
    {
      "previsaoTitulo": "Título Previsto"
    }
    ```

### Como Funciona a Previsão de Títulos

A previsão de títulos é feita através de um modelo de Machine Learning treinado utilizando dados históricos de livros cadastrados. Quando um usuário envia informações de um livro (como autor, gênero, ano de publicação, e classificação), o modelo retorna uma previsão do título do livro.

O processo de treinamento do modelo é iniciado caso o modelo ainda não esteja presente no sistema, e o treinamento é feito com dados extraídos do banco de dados de livros.

## Autenticação com JWT

A API utiliza JWT para autenticação. Para acessar as rotas protegidas, é necessário passar um token JWT no cabeçalho da requisição.

- **POST** `/api/auth/login`
  - **Descrição:** Realiza o login e gera um token JWT.
  - **Corpo da Requisição (JSON):**
    ```json
    {
      "usuario": "usuario_teste",
      "senha": "senha_teste"
    }
    ```
  - **Resposta (JSON):**
    ```json
    {
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6InVz..."
    }
    ```

Após obter o token, ele deve ser incluído no cabeçalho `Authorization` das requisições subsequentes.

## Conclusão

Este projeto tem como objetivo facilitar o cadastro de livros em bibliotecas e possibilitar previsões automatizadas de títulos de livros, utilizando técnicas de Machine Learning para melhorar a organização e a recomendação de livros. A API oferece rotas RESTful e segurança através de JWT, tornando-a acessível e segura para diferentes cenários de uso.
