# Azure Functions in C#

Just learning and trying stuff.

Working from a template and [starting tutorial](https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp), then [writing to Storage Queues](https://learn.microsoft.com/en-us/azure/azure-functions/functions-add-output-binding-storage-queue-vs-code?pivots=programming-language-csharp&tabs=isolated-process%2Cv1).

## Run locally

```bash
dotnet clean
dotnet build
pushd src && func start && popd
```

Then test with something like

    curl --header "Content-Type: application/json" -X POST localhost:7071/api/httpexample --data '{"id": 409, "quantity": -2}'


## Deploying to Azure

[See this for VS Code](https://learn.microsoft.com/en-us/azure/azure-functions/functions-add-output-binding-storage-queue-vs-code?pivots=programming-language-csharp&tabs=isolated-process%2Cv1#redeploy-and-verify-the-updated-app).

Todo: deploy with GitHub Actions.

### Running on Azure 

See `Function App` in Azure Portal and set the `FUNCTION_URL` variable.

    curl --header "Content-Type: application/json" -X POST ${FUNCTION_URL}/api/httpexample --data '{"id": 409, "quantity": -2}'
    

## Testing

```
pushd tests && dotnet test && popd
```