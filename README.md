# Api setup


```
cd ./api/api
```

Создать .env (можно скопировать содержимое из .env.example)

```
dotnet install

dotnet ef migration update --context TodoTaskContext

dotnet run

```

# Client setup


```
cd ./client
```

Создать .env (можно скопировать содержимое из .env.example)

```
pnpm install

pnpm dev
```
