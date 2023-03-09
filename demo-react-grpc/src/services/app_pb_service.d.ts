// package: app
// file: app.proto

import * as app_pb from "./app_pb";
import {grpc} from "@improbable-eng/grpc-web";

type AppUnaryCall = {
  readonly methodName: string;
  readonly service: typeof App;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof app_pb.AppRequest;
  readonly responseType: typeof app_pb.AppResponse;
};

type AppStreamingFromServer = {
  readonly methodName: string;
  readonly service: typeof App;
  readonly requestStream: false;
  readonly responseStream: true;
  readonly requestType: typeof app_pb.AppRequest;
  readonly responseType: typeof app_pb.AppResponse;
};

type AppStreamingFromClient = {
  readonly methodName: string;
  readonly service: typeof App;
  readonly requestStream: true;
  readonly responseStream: false;
  readonly requestType: typeof app_pb.AppRequest;
  readonly responseType: typeof app_pb.AppResponse;
};

type AppStreamingBothWays = {
  readonly methodName: string;
  readonly service: typeof App;
  readonly requestStream: true;
  readonly responseStream: true;
  readonly requestType: typeof app_pb.AppRequest;
  readonly responseType: typeof app_pb.AppResponse;
};

type AppDoValue = {
  readonly methodName: string;
  readonly service: typeof App;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof app_pb.AppDoValueRequest;
  readonly responseType: typeof app_pb.AppDoValueResponse;
};

type AppDoUpload = {
  readonly methodName: string;
  readonly service: typeof App;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof app_pb.AppDoUploadRequest;
  readonly responseType: typeof app_pb.AppDoUploadResponse;
};

type AppDoDownload = {
  readonly methodName: string;
  readonly service: typeof App;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof app_pb.AppDoDownloadRequest;
  readonly responseType: typeof app_pb.AppDoDownloadResponse;
};

type AppDoUploadStream = {
  readonly methodName: string;
  readonly service: typeof App;
  readonly requestStream: true;
  readonly responseStream: false;
  readonly requestType: typeof app_pb.AppDoUploadStreamRequest;
  readonly responseType: typeof app_pb.AppDoUploadStreamResponse;
};

type AppDoDownloadStream = {
  readonly methodName: string;
  readonly service: typeof App;
  readonly requestStream: false;
  readonly responseStream: true;
  readonly requestType: typeof app_pb.AppDoDownloadStreamRequest;
  readonly responseType: typeof app_pb.AppDoDownloadStreamResponse;
};

export class App {
  static readonly serviceName: string;
  static readonly UnaryCall: AppUnaryCall;
  static readonly StreamingFromServer: AppStreamingFromServer;
  static readonly StreamingFromClient: AppStreamingFromClient;
  static readonly StreamingBothWays: AppStreamingBothWays;
  static readonly DoValue: AppDoValue;
  static readonly DoUpload: AppDoUpload;
  static readonly DoDownload: AppDoDownload;
  static readonly DoUploadStream: AppDoUploadStream;
  static readonly DoDownloadStream: AppDoDownloadStream;
}

export type ServiceError = { message: string, code: number; metadata: grpc.Metadata }
export type Status = { details: string, code: number; metadata: grpc.Metadata }

interface UnaryResponse {
  cancel(): void;
}
interface ResponseStream<T> {
  cancel(): void;
  on(type: 'data', handler: (message: T) => void): ResponseStream<T>;
  on(type: 'end', handler: (status?: Status) => void): ResponseStream<T>;
  on(type: 'status', handler: (status: Status) => void): ResponseStream<T>;
}
interface RequestStream<T> {
  write(message: T): RequestStream<T>;
  end(): void;
  cancel(): void;
  on(type: 'end', handler: (status?: Status) => void): RequestStream<T>;
  on(type: 'status', handler: (status: Status) => void): RequestStream<T>;
}
interface BidirectionalStream<ReqT, ResT> {
  write(message: ReqT): BidirectionalStream<ReqT, ResT>;
  end(): void;
  cancel(): void;
  on(type: 'data', handler: (message: ResT) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'end', handler: (status?: Status) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'status', handler: (status: Status) => void): BidirectionalStream<ReqT, ResT>;
}

export class AppClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  unaryCall(
    requestMessage: app_pb.AppRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: app_pb.AppResponse|null) => void
  ): UnaryResponse;
  unaryCall(
    requestMessage: app_pb.AppRequest,
    callback: (error: ServiceError|null, responseMessage: app_pb.AppResponse|null) => void
  ): UnaryResponse;
  streamingFromServer(requestMessage: app_pb.AppRequest, metadata?: grpc.Metadata): ResponseStream<app_pb.AppResponse>;
  streamingFromClient(metadata?: grpc.Metadata): RequestStream<app_pb.AppRequest>;
  streamingBothWays(metadata?: grpc.Metadata): BidirectionalStream<app_pb.AppRequest, app_pb.AppResponse>;
  doValue(
    requestMessage: app_pb.AppDoValueRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: app_pb.AppDoValueResponse|null) => void
  ): UnaryResponse;
  doValue(
    requestMessage: app_pb.AppDoValueRequest,
    callback: (error: ServiceError|null, responseMessage: app_pb.AppDoValueResponse|null) => void
  ): UnaryResponse;
  doUpload(
    requestMessage: app_pb.AppDoUploadRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: app_pb.AppDoUploadResponse|null) => void
  ): UnaryResponse;
  doUpload(
    requestMessage: app_pb.AppDoUploadRequest,
    callback: (error: ServiceError|null, responseMessage: app_pb.AppDoUploadResponse|null) => void
  ): UnaryResponse;
  doDownload(
    requestMessage: app_pb.AppDoDownloadRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: app_pb.AppDoDownloadResponse|null) => void
  ): UnaryResponse;
  doDownload(
    requestMessage: app_pb.AppDoDownloadRequest,
    callback: (error: ServiceError|null, responseMessage: app_pb.AppDoDownloadResponse|null) => void
  ): UnaryResponse;
  doUploadStream(metadata?: grpc.Metadata): RequestStream<app_pb.AppDoUploadStreamRequest>;
  doDownloadStream(requestMessage: app_pb.AppDoDownloadStreamRequest, metadata?: grpc.Metadata): ResponseStream<app_pb.AppDoDownloadStreamResponse>;
}

