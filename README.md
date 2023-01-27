# CRUDIntermediario
CRUD de nível intermediario desenvolvido com template web do **ASP .NET 5** utilizando o **EntityFramework** e o gerênciador de pacotes **libman**.
___

## Dependências

ASP .NET version 5.0.408

```sh
dotnet new web --no-https --framework net5.0
```

/properties

```sh
dotnet new globaljson --sdk-version 5.0.408
```

jsdelivr

```sh
libman init -p jsdelivr
```

Bootstrap5

```sh
libman install bootstrap -d wwwroot/lib/bootstrap5
```
___

## Funcionalidades

- Listar, Cadastrar, Editar e Excluir registros de Produtos;
- Listar, Cadastrar, Editar e Excluir registros de  Categorias de Produtos;