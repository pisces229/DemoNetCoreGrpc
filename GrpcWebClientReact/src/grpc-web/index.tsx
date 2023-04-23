import { RunnerClient } from "./services/runner_grpc_web_pb";
import { RunnerRequest } from "./services/runner_pb";
import CONFIG from '@/config';

const Index = () => {
  const onClickRun = () => {
    let client = new RunnerClient(CONFIG.ENVIRONMENT);
    let request = new RunnerRequest();
    request.setMessage(new Date().toISOString());
    client.run(request, { 'bearer': new Date().toISOString() }, (error, reply) => {
      if (reply) {
        console.log(reply?.getMessage());
      }
      if (error) {
        console.error(error);
      }
    });
  };
  const onClickServerStreaming = () => {
    let client = new RunnerClient(CONFIG.ENVIRONMENT);
    let request = new RunnerRequest();
    request.setMessage(new Date().toISOString());
    let count = 0;
    let streaming = client.serverStreaming(request, { 'bearer': new Date().toISOString() })
    .on('error', (value) => {
      console.log('error', value);
    })
    .on('status', (value) => {
      console.log('status', value);
    })
    .on('metadata', (value) => {
      console.log('metadata', value);
    })
    .on('data', (value) => {
      console.log('data', value?.getMessage());
      if (++count == 5) {
        streaming.cancel();
      }
    })
    .on('end', () => {
      console.log('end');
    });
  };
  return (
    <>
      <h3>grpc-web</h3>
      <button onClick={onClickRun}>Run</button>
      <button onClick={onClickServerStreaming}>Server Streaming</button>
    </>
  );
}
export default Index;