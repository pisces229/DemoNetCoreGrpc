import React, { useState } from 'react';
import './App.css';
import { AppDoDownloadRequest, AppDoUploadRequest, AppDoValueRequest, AppRequest } from './services/app_pb';
import { AppClient } from './services/app_pb_service';
import { HelloRequest } from './services/greet_pb';
import { GreeterClient } from './services/greet_pb_service';

const HOST = 'https://localhost:7059';

export const App = () => {
  // Greeter
  const onClickGreeterSayHello = () => {
    let client = new GreeterClient(HOST);
    let request = new HelloRequest();
    request.setName('React');
    client.sayHello(request, (error, reply) => {
      if (reply) {
        console.log(reply.getMessage());
      }
      if (error) {
        console.error(error);
      }
    });
  };
  // App 
  const onClickAppUnaryCall = () => {
    let client = new AppClient(HOST);
    let request = new AppRequest();
    request.setMessage(new Date().toString());
    client.unaryCall(request, (error, reply) => {
      if (reply) {
        console.log(reply?.getMessage());
      }
      if (error) {
        console.error(error);
      }
    });
  };
  const onClickAppStreamingFromServer = () => {
    let client = new AppClient(HOST);
    let request = new AppRequest();
    request.setMessage(new Date().toString());
    let streaming = client.streamingFromServer(request)
    .on('data', (messgae) => {
      console.log('data', messgae.getMessage());
    })
    .on('end', (status) => {
      console.log('end', status);
    })
    .on('status', (status) => {
      console.log('status', status);
    });
    setTimeout(() => streaming.cancel(), 5000);
  };
  const onClickAppStreamingFromClient = () => {
    let client = new AppClient(HOST);
    let request = new AppRequest();
    request.setMessage(new Date().toString());
    let streaming = client.streamingFromClient()
    .write(request)
    .write(request)
    .on('end', (status) => {
      console.log('end', status);
    })
    .on('status', (status) => {
      console.log('status', status);
    });
    // setTimeout(() => streaming.cancel(), 5000);
    setTimeout(() => streaming.end(), 5000);
  };
  const onClickAppStreamingBothWays = () => {
    let client = new AppClient(HOST);
    let request = new AppRequest();
    request.setMessage(new Date().toString());
    let streaming = client.streamingBothWays()
    .on('data', (messgae) => {
      console.log('data', messgae.getMessage());
    })
    .on('end', (status) => {
      console.log('end', status);
    })
    .on('status', (status) => {
      console.log('status', status);
    })
    .write(request)
    .write(request);
    // setTimeout(() => streaming.cancel(), 5000);
    setTimeout(() => streaming.end(), 5000);
  };
  const onClickAppDoValue = () => {
    let client = new AppClient(HOST);
    let request = new AppDoValueRequest();
    request.setBoolvalue(true);
    request.setIntvalue(1);
    request.setDoublevalue(1.1);
    request.setStringvalue(Date.now().toString());
    client.doValue(request, (error, reply) => {
      if (reply) {
        console.log(reply.getBoolvalue());
        console.log(reply.getIntvalue());
        console.log(reply.getDoublevalue());
        console.log(reply.getStringvalue());
      }
      if (error) {
        console.error(error);
      }
    });
  };
  const [file, setFile] = useState<FileList | null>();
  const onChangeFile = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files?.length! > 0) {
      setFile(e.target.files);
    } else {
      setFile(null);
    }
  };
  const onClickAppDoUpload = async () => {
    let client = new AppClient(HOST);
    let request = new AppDoUploadRequest();
    let item = file!.item(0)!;
    request.setName(item.name);
    request.setData((await item.stream().getReader().read().then(value => value))!.value!);
    client.doUpload(request, (error, reply) => {
      if (reply) {
        console.log(reply.getMessage());
      }
      if (error) {
        console.error(error);
      }
    });
  };
  const onClickAppDoDownload = async () => {
    let client = new AppClient(HOST);
    let request = new AppDoDownloadRequest();
    request.setName(Date.now().toString());
    client.doDownload(request, (error, reply) => {
      if (reply) {
        console.log(reply.getName());
        console.log(new TextDecoder("utf-8").decode(reply.getData_asU8()!));
      }
      if (error) {
        console.error(error);
      }
    });
  };
  return (
    <>
      <h1>Demo React gRPC</h1>
      <h2>Greeter</h2>
      <button onClick={onClickGreeterSayHello}>SayHello</button>
      <h3>App</h3>
      <button onClick={onClickAppUnaryCall}>UnaryCall</button>
      <button onClick={onClickAppStreamingFromServer}>StreamingFromServer</button>
      <button onClick={onClickAppStreamingFromClient}>StreamingFromClient</button>
      <button onClick={onClickAppStreamingBothWays}>StreamingBothWays</button>
      <h3>App</h3>
      <button onClick={onClickAppDoValue}>DoValue</button>
      <input type="file" multiple onChange={onChangeFile}></input>
      <button onClick={onClickAppDoUpload}>DoUpload</button>
      <button onClick={onClickAppDoDownload}>DoDownload</button>
    </>
  );
}