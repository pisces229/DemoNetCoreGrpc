import React from 'react';
import ReactDOM from 'react-dom/client';
import '@/index.css';
import GrcpWebIndex from '@/grpc-web';
import ImproabcleEngGrcpWebIndex from '@/improbable-eng-grpc-web';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement,
);

root.render(
  <React.StrictMode>
    <GrcpWebIndex />
    <ImproabcleEngGrcpWebIndex />
  </React.StrictMode>,
);