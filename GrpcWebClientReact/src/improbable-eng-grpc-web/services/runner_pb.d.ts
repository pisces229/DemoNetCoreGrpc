// package: runner
// file: runner.proto

import * as jspb from "google-protobuf";

export class RunnerRequest extends jspb.Message {
  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): RunnerRequest.AsObject;
  static toObject(includeInstance: boolean, msg: RunnerRequest): RunnerRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: RunnerRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): RunnerRequest;
  static deserializeBinaryFromReader(message: RunnerRequest, reader: jspb.BinaryReader): RunnerRequest;
}

export namespace RunnerRequest {
  export type AsObject = {
    message: string,
  }
}

export class RunnerResponse extends jspb.Message {
  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): RunnerResponse.AsObject;
  static toObject(includeInstance: boolean, msg: RunnerResponse): RunnerResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: RunnerResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): RunnerResponse;
  static deserializeBinaryFromReader(message: RunnerResponse, reader: jspb.BinaryReader): RunnerResponse;
}

export namespace RunnerResponse {
  export type AsObject = {
    message: string,
  }
}

