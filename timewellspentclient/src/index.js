import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import firebase from "firebase/compat/app";
import { firebaseConfig } from "./apiKeys"
import { BrowserRouter } from 'react-router-dom';
import { TimeWellSpent } from './TimeWellSpent';
import 'bootstrap/dist/css/bootstrap.min.css';

firebase.initializeApp(firebaseConfig);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <BrowserRouter>
    <TimeWellSpent />
  </BrowserRouter>
);

