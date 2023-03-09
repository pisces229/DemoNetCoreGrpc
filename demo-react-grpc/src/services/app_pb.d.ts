// package: app
// file: app.proto

import * as jspb from "google-protobuf";

export class AppRequest extends jspb.Message {
  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppRequest.AsObject;
  static toObject(includeInstance: boolean, msg: AppRequest): AppRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppRequest;
  static deserializeBinaryFromReader(message: AppRequest, reader: jspb.BinaryReader): AppRequest;
}

export namespace AppRequest {
  export type AsObject = {
    message: string,
  }
}

export class AppResponse extends jspb.Message {
  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppResponse.AsObject;
  static toObject(includeInstance: boolean, msg: AppResponse): AppResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppResponse;
  static deserializeBinaryFromReader(message: AppResponse, reader: jspb.BinaryReader): AppResponse;
}

export namespace AppResponse {
  export type AsObject = {
    message: string,
  }
}

export class AppDoValueRequest extends jspb.Message {
  getBoolvalue(): boolean;
  setBoolvalue(value: boolean): void;

  getIntvalue(): number;
  setIntvalue(value: number): void;

  getDoublevalue(): number;
  setDoublevalue(value: number): void;

  getStringvalue(): string;
  setStringvalue(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppDoValueRequest.AsObject;
  static toObject(includeInstance: boolean, msg: AppDoValueRequest): AppDoValueRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppDoValueRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppDoValueRequest;
  static deserializeBinaryFromReader(message: AppDoValueRequest, reader: jspb.BinaryReader): AppDoValueRequest;
}

export namespace AppDoValueRequest {
  export type AsObject = {
    boolvalue: boolean,
    intvalue: number,
    doublevalue: number,
    stringvalue: string,
  }
}

export class AppDoValueResponse extends jspb.Message {
  getBoolvalue(): boolean;
  setBoolvalue(value: boolean): void;

  getIntvalue(): number;
  setIntvalue(value: number): void;

  getDoublevalue(): number;
  setDoublevalue(value: number): void;

  getStringvalue(): string;
  setStringvalue(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppDoValueResponse.AsObject;
  static toObject(includeInstance: boolean, msg: AppDoValueResponse): AppDoValueResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppDoValueResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppDoValueResponse;
  static deserializeBinaryFromReader(message: AppDoValueResponse, reader: jspb.BinaryReader): AppDoValueResponse;
}

export namespace AppDoValueResponse {
  export type AsObject = {
    boolvalue: boolean,
    intvalue: number,
    doublevalue: number,
    stringvalue: string,
  }
}

export class AppDoUploadRequest extends jspb.Message {
  getName(): string;
  setName(value: string): void;

  getData(): Uint8Array | string;
  getData_asU8(): Uint8Array;
  getData_asB64(): string;
  setData(value: Uint8Array | string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppDoUploadRequest.AsObject;
  static toObject(includeInstance: boolean, msg: AppDoUploadRequest): AppDoUploadRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppDoUploadRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppDoUploadRequest;
  static deserializeBinaryFromReader(message: AppDoUploadRequest, reader: jspb.BinaryReader): AppDoUploadRequest;
}

export namespace AppDoUploadRequest {
  export type AsObject = {
    name: string,
    data: Uint8Array | string,
  }
}

export class AppDoUploadResponse extends jspb.Message {
  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppDoUploadResponse.AsObject;
  static toObject(includeInstance: boolean, msg: AppDoUploadResponse): AppDoUploadResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppDoUploadResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppDoUploadResponse;
  static deserializeBinaryFromReader(message: AppDoUploadResponse, reader: jspb.BinaryReader): AppDoUploadResponse;
}

export namespace AppDoUploadResponse {
  export type AsObject = {
    message: string,
  }
}

export class AppDoDownloadRequest extends jspb.Message {
  getName(): string;
  setName(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppDoDownloadRequest.AsObject;
  static toObject(includeInstance: boolean, msg: AppDoDownloadRequest): AppDoDownloadRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppDoDownloadRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppDoDownloadRequest;
  static deserializeBinaryFromReader(message: AppDoDownloadRequest, reader: jspb.BinaryReader): AppDoDownloadRequest;
}

export namespace AppDoDownloadRequest {
  export type AsObject = {
    name: string,
  }
}

export class AppDoDownloadResponse extends jspb.Message {
  getName(): string;
  setName(value: string): void;

  getData(): Uint8Array | string;
  getData_asU8(): Uint8Array;
  getData_asB64(): string;
  setData(value: Uint8Array | string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppDoDownloadResponse.AsObject;
  static toObject(includeInstance: boolean, msg: AppDoDownloadResponse): AppDoDownloadResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppDoDownloadResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppDoDownloadResponse;
  static deserializeBinaryFromReader(message: AppDoDownloadResponse, reader: jspb.BinaryReader): AppDoDownloadResponse;
}

export namespace AppDoDownloadResponse {
  export type AsObject = {
    name: string,
    data: Uint8Array | string,
  }
}

export class AppDoUploadStreamRequest extends jspb.Message {
  getData(): Uint8Array | string;
  getData_asU8(): Uint8Array;
  getData_asB64(): string;
  setData(value: Uint8Array | string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppDoUploadStreamRequest.AsObject;
  static toObject(includeInstance: boolean, msg: AppDoUploadStreamRequest): AppDoUploadStreamRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppDoUploadStreamRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppDoUploadStreamRequest;
  static deserializeBinaryFromReader(message: AppDoUploadStreamRequest, reader: jspb.BinaryReader): AppDoUploadStreamRequest;
}

export namespace AppDoUploadStreamRequest {
  export type AsObject = {
    data: Uint8Array | string,
  }
}

export class AppDoUploadStreamResponse extends jspb.Message {
  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppDoUploadStreamResponse.AsObject;
  static toObject(includeInstance: boolean, msg: AppDoUploadStreamResponse): AppDoUploadStreamResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppDoUploadStreamResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppDoUploadStreamResponse;
  static deserializeBinaryFromReader(message: AppDoUploadStreamResponse, reader: jspb.BinaryReader): AppDoUploadStreamResponse;
}

export namespace AppDoUploadStreamResponse {
  export type AsObject = {
    message: string,
  }
}

export class AppDoDownloadStreamRequest extends jspb.Message {
  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppDoDownloadStreamRequest.AsObject;
  static toObject(includeInstance: boolean, msg: AppDoDownloadStreamRequest): AppDoDownloadStreamRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppDoDownloadStreamRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppDoDownloadStreamRequest;
  static deserializeBinaryFromReader(message: AppDoDownloadStreamRequest, reader: jspb.BinaryReader): AppDoDownloadStreamRequest;
}

export namespace AppDoDownloadStreamRequest {
  export type AsObject = {
    message: string,
  }
}

export class AppDoDownloadStreamResponse extends jspb.Message {
  getData(): Uint8Array | string;
  getData_asU8(): Uint8Array;
  getData_asB64(): string;
  setData(value: Uint8Array | string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AppDoDownloadStreamResponse.AsObject;
  static toObject(includeInstance: boolean, msg: AppDoDownloadStreamResponse): AppDoDownloadStreamResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AppDoDownloadStreamResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AppDoDownloadStreamResponse;
  static deserializeBinaryFromReader(message: AppDoDownloadStreamResponse, reader: jspb.BinaryReader): AppDoDownloadStreamResponse;
}

export namespace AppDoDownloadStreamResponse {
  export type AsObject = {
    data: Uint8Array | string,
  }
}

