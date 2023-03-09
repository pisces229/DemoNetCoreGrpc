// package: runner
// file: runner.proto

var runner_pb = require("./runner_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var Runner = (function () {
  function Runner() {}
  Runner.serviceName = "runner.Runner";
  return Runner;
}());

Runner.Run = {
  methodName: "Run",
  service: Runner,
  requestStream: false,
  responseStream: false,
  requestType: runner_pb.RunnerRequest,
  responseType: runner_pb.RunnerResponse
};

Runner.ServerStreaming = {
  methodName: "ServerStreaming",
  service: Runner,
  requestStream: false,
  responseStream: true,
  requestType: runner_pb.RunnerRequest,
  responseType: runner_pb.RunnerResponse
};

exports.Runner = Runner;

function RunnerClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

RunnerClient.prototype.run = function run(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(Runner.Run, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

RunnerClient.prototype.serverStreaming = function serverStreaming(requestMessage, metadata) {
  var listeners = {
    data: [],
    end: [],
    status: []
  };
  var client = grpc.invoke(Runner.ServerStreaming, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onMessage: function (responseMessage) {
      listeners.data.forEach(function (handler) {
        handler(responseMessage);
      });
    },
    onEnd: function (status, statusMessage, trailers) {
      listeners.status.forEach(function (handler) {
        handler({ code: status, details: statusMessage, metadata: trailers });
      });
      listeners.end.forEach(function (handler) {
        handler({ code: status, details: statusMessage, metadata: trailers });
      });
      listeners = null;
    }
  });
  return {
    on: function (type, handler) {
      listeners[type].push(handler);
      return this;
    },
    cancel: function () {
      listeners = null;
      client.close();
    }
  };
};

exports.RunnerClient = RunnerClient;

