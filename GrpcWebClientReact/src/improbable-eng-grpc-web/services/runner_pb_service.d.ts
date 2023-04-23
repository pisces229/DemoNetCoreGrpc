// package: runner
// file: runner.proto

import * as runner_pb from "./runner_pb";
import {grpc} from "@improbable-eng/grpc-web";

type RunnerRun = {
  readonly methodName: string;
  readonly service: typeof Runner;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof runner_pb.RunnerRequest;
  readonly responseType: typeof runner_pb.RunnerResponse;
};

type RunnerServerStreaming = {
  readonly methodName: string;
  readonly service: typeof Runner;
  readonly requestStream: false;
  readonly responseStream: true;
  readonly requestType: typeof runner_pb.RunnerRequest;
  readonly responseType: typeof runner_pb.RunnerResponse;
};

export class Runner {
  static readonly serviceName: string;
  static readonly Run: RunnerRun;
  static readonly ServerStreaming: RunnerServerStreaming;
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

export class RunnerClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  run(
    requestMessage: runner_pb.RunnerRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: runner_pb.RunnerResponse|null) => void
  ): UnaryResponse;
  run(
    requestMessage: runner_pb.RunnerRequest,
    callback: (error: ServiceError|null, responseMessage: runner_pb.RunnerResponse|null) => void
  ): UnaryResponse;
  serverStreaming(requestMessage: runner_pb.RunnerRequest, metadata?: grpc.Metadata): ResponseStream<runner_pb.RunnerResponse>;
}

