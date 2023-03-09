// package: app
// file: app.proto

var app_pb = require("./app_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var App = (function () {
  function App() {}
  App.serviceName = "app.App";
  return App;
}());

App.UnaryCall = {
  methodName: "UnaryCall",
  service: App,
  requestStream: false,
  responseStream: false,
  requestType: app_pb.AppRequest,
  responseType: app_pb.AppResponse
};

App.StreamingFromServer = {
  methodName: "StreamingFromServer",
  service: App,
  requestStream: false,
  responseStream: true,
  requestType: app_pb.AppRequest,
  responseType: app_pb.AppResponse
};

App.StreamingFromClient = {
  methodName: "StreamingFromClient",
  service: App,
  requestStream: true,
  responseStream: false,
  requestType: app_pb.AppRequest,
  responseType: app_pb.AppResponse
};

App.StreamingBothWays = {
  methodName: "StreamingBothWays",
  service: App,
  requestStream: true,
  responseStream: true,
  requestType: app_pb.AppRequest,
  responseType: app_pb.AppResponse
};

App.DoValue = {
  methodName: "DoValue",
  service: App,
  requestStream: false,
  responseStream: false,
  requestType: app_pb.AppDoValueRequest,
  responseType: app_pb.AppDoValueResponse
};

App.DoUpload = {
  methodName: "DoUpload",
  service: App,
  requestStream: false,
  responseStream: false,
  requestType: app_pb.AppDoUploadRequest,
  responseType: app_pb.AppDoUploadResponse
};

App.DoDownload = {
  methodName: "DoDownload",
  service: App,
  requestStream: false,
  responseStream: false,
  requestType: app_pb.AppDoDownloadRequest,
  responseType: app_pb.AppDoDownloadResponse
};

App.DoUploadStream = {
  methodName: "DoUploadStream",
  service: App,
  requestStream: true,
  responseStream: false,
  requestType: app_pb.AppDoUploadStreamRequest,
  responseType: app_pb.AppDoUploadStreamResponse
};

App.DoDownloadStream = {
  methodName: "DoDownloadStream",
  service: App,
  requestStream: false,
  responseStream: true,
  requestType: app_pb.AppDoDownloadStreamRequest,
  responseType: app_pb.AppDoDownloadStreamResponse
};

exports.App = App;

function AppClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

AppClient.prototype.unaryCall = function unaryCall(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(App.UnaryCall, {
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

AppClient.prototype.streamingFromServer = function streamingFromServer(requestMessage, metadata) {
  var listeners = {
    data: [],
    end: [],
    status: []
  };
  var client = grpc.invoke(App.StreamingFromServer, {
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

AppClient.prototype.streamingFromClient = function streamingFromClient(metadata) {
  var listeners = {
    end: [],
    status: []
  };
  var client = grpc.client(App.StreamingFromClient, {
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport
  });
  client.onEnd(function (status, statusMessage, trailers) {
    listeners.status.forEach(function (handler) {
      handler({ code: status, details: statusMessage, metadata: trailers });
    });
    listeners.end.forEach(function (handler) {
      handler({ code: status, details: statusMessage, metadata: trailers });
    });
    listeners = null;
  });
  return {
    on: function (type, handler) {
      listeners[type].push(handler);
      return this;
    },
    write: function (requestMessage) {
      if (!client.started) {
        client.start(metadata);
      }
      client.send(requestMessage);
      return this;
    },
    end: function () {
      client.finishSend();
    },
    cancel: function () {
      listeners = null;
      client.close();
    }
  };
};

AppClient.prototype.streamingBothWays = function streamingBothWays(metadata) {
  var listeners = {
    data: [],
    end: [],
    status: []
  };
  var client = grpc.client(App.StreamingBothWays, {
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport
  });
  client.onEnd(function (status, statusMessage, trailers) {
    listeners.status.forEach(function (handler) {
      handler({ code: status, details: statusMessage, metadata: trailers });
    });
    listeners.end.forEach(function (handler) {
      handler({ code: status, details: statusMessage, metadata: trailers });
    });
    listeners = null;
  });
  client.onMessage(function (message) {
    listeners.data.forEach(function (handler) {
      handler(message);
    })
  });
  client.start(metadata);
  return {
    on: function (type, handler) {
      listeners[type].push(handler);
      return this;
    },
    write: function (requestMessage) {
      client.send(requestMessage);
      return this;
    },
    end: function () {
      client.finishSend();
    },
    cancel: function () {
      listeners = null;
      client.close();
    }
  };
};

AppClient.prototype.doValue = function doValue(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(App.DoValue, {
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

AppClient.prototype.doUpload = function doUpload(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(App.DoUpload, {
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

AppClient.prototype.doDownload = function doDownload(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(App.DoDownload, {
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

AppClient.prototype.doUploadStream = function doUploadStream(metadata) {
  var listeners = {
    end: [],
    status: []
  };
  var client = grpc.client(App.DoUploadStream, {
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport
  });
  client.onEnd(function (status, statusMessage, trailers) {
    listeners.status.forEach(function (handler) {
      handler({ code: status, details: statusMessage, metadata: trailers });
    });
    listeners.end.forEach(function (handler) {
      handler({ code: status, details: statusMessage, metadata: trailers });
    });
    listeners = null;
  });
  return {
    on: function (type, handler) {
      listeners[type].push(handler);
      return this;
    },
    write: function (requestMessage) {
      if (!client.started) {
        client.start(metadata);
      }
      client.send(requestMessage);
      return this;
    },
    end: function () {
      client.finishSend();
    },
    cancel: function () {
      listeners = null;
      client.close();
    }
  };
};

AppClient.prototype.doDownloadStream = function doDownloadStream(requestMessage, metadata) {
  var listeners = {
    data: [],
    end: [],
    status: []
  };
  var client = grpc.invoke(App.DoDownloadStream, {
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

exports.AppClient = AppClient;

