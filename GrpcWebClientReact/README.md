# Demo React gRPC 

##  Marketplace

[vscode-proto3](https://marketplace.visualstudio.com/items?itemName=zxh404.vscode-proto3)

## [improbable-eng grpc-web](https://github.com/improbable-eng/grpc-web)

[0](https://www.cnblogs.com/ElderJames/p/call-asp_net_core-grpc-web-with-js-and-ts.html)

`npm i protoc google-protobuf ts-protoc-gen @improbable-eng/grpc-web`

```
./node_modules/protoc/protoc/bin/protoc `
-I src/protos src/protos/*.proto `
--plugin=protoc-gen-ts=.\node_modules\.bin\protoc-gen-ts.cmd `
--js_out=import_style=commonjs,binary:src/improbable-eng-grpc-web/services `
--ts_out=service=grpc-web:src/improbable-eng-grpc-web/services
```

## [grpc-web](https://github.com/grpc/grpc-web)

[0](https://static.kancloud.cn/cyyspring/webpack/3091261)

`npm i google-protobuf grpc-web`

```
protoc `
-I src/protos src/protos/*.proto `
--plugin=protoc-gen-grpc=./protoc-gen-grpc-web.exe `
--js_out=import_style=commonjs:src/grpc-web/services `
--grpc-web_out=import_style=commonjs+dts,mode=grpcwebtext:src/grpc-web/services 
```

## reference

[The state of gRPC in the browser](https://grpc.io/blog/state-of-grpc-web/)

[gRPC-Web in ASP.NET Core gRPC apps](https://learn.microsoft.com/aspnet/core/grpc/grpcweb)

[protobuf](https://github.com/protocolbuffers/protobuf)

[grpc-web](https://github.com/grpc/grpc-web)
