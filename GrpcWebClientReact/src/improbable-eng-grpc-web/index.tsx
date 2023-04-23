import { grpc } from '@improbable-eng/grpc-web';
import { RunnerClient } from "./services/runner_pb_service";
import { RunnerRequest } from "./services/runner_pb";
import CONFIG from '@/config';

const Index = () => {
  const onClickRun = () => {
    let client = new RunnerClient(CONFIG.ENVIRONMENT);
    let request = new RunnerRequest();
    request.setMessage(new Date().toISOString());
    let metadata = new grpc.Metadata();
    metadata.set('bearer', new Date().toISOString());
    client.run(request, metadata,
      (error, reply) => {
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
    let metadata = new grpc.Metadata();
    metadata.set('bearer', new Date().toISOString());
    let count = 0;
    let streaming = client.serverStreaming(request, metadata)
    .on('data', (value) => {
      console.log('data', value?.getMessage());
      if (++count == 5) {
        streaming.cancel();
      }
    })
    .on('end', (value) => {
      console.log('end', value);
    })
    .on('status', (value) => {
      console.log('status', value);
    });
  };
  return (
    <>
      <h3>improbable-eng-grpc-web</h3>
      <button onClick={onClickRun}>Run</button>
      <button onClick={onClickServerStreaming}>Server Streaming</button>
    </>
  );
}
export default Index;