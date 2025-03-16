See https://patrickhuber.github.io/2017/07/26/avoid-secrets-in-dot-net-core-tests.html for user secrets in tests

dotnet user-secrets set SFUsername "xxx"
dotnet user-secrets set SFPassword "xxx"
dotnet user-secrets set SFToken "xxxx"
dotnet user-secrets set SFClientId "xxxx"
dotnet user-secrets set SFClientSecret "xxxx"

See provided appsettings.Development.json for values.