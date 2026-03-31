# Api setup

.net8.0
```
cd ./api/api
```

Создать .env (можно скопировать содержимое из .env.example)
```
cp .env.example .env
```
Установка
```
dotnet restore

dotnet ef database update --context TodoTaskContext

dotnet run
```

# Client setup

```
cd ./client
```

Создать .env (можно скопировать содержимое из .env.example)
```
cp .env.example .env
```

```
pnpm install

pnpm dev
```
