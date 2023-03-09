# Demo React gRPC 

## create-react-app

`npx create-react-app <name> --template typescript`

## [ASP.NET Core gRPC-Web with js/ts](https://www.cnblogs.com/ElderJames/p/call-asp_net_core-grpc-web-with-js-and-ts.html)

`npm i protoc google-protobuf ts-protoc-gen @improbable-eng/grpc-web`

```
./node_modules/protoc/protoc/bin/protoc --plugin="protoc-gen-ts=.\node_modules\.bin\protoc-gen-ts.cmd" --js_out="import_style=commonjs,binary:src/services" --ts_out="service=grpc-web:src/services" -I src/protos src/protos/*.proto
```

## [gRPC-Web in browser](https://static.kancloud.cn/cyyspring/webpack/3091261)

`npm i google-protobuf grpc-web`

```
protoc -I=./ *.proto --js_out=import_style=commonjs:./services/ --plugin=protoc-gen-grpc=./protoc-gen-grpc-web.exe --grpc-web_out=import_style=commonjs+dts,mode=grpcwebtext:./services/
```

## reference

[The state of gRPC in the browser](https://grpc.io/blog/state-of-grpc-web/)

[gRPC-Web in ASP.NET Core gRPC apps](https://learn.microsoft.com/aspnet/core/grpc/grpcweb)

[protobuf](https://github.com/protocolbuffers/protobuf)

[grpc-web](https://github.com/grpc/grpc-web)
